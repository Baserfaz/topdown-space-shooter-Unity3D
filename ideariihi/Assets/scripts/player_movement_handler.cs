using UnityEngine;
using System.Collections;

public class player_movement_handler : MonoBehaviour {

    private Rigidbody2D myBody;
    public float movementSpeedMultiplier = 5f;
    public float maxMovementSpeed = 10f;

    public float thrusterFuelDegenRate = 2f;
	public float thrusterFuelRegenRate = 5f;
    public float MaxThrusterFuel = 100f;
    
	public float turningSpeed = 1f;
	public bool instantTurning = false;

    private float currentThrusterFuel;
    private float currentMovementSpeed = 0f;

    private GameObject GameMasterGo;
    private guiManager GuiManager;
    private game_settings GameSettings;
    private GameObject_handler GameObjectHandler;

    private GameObject myJetInst;
    private ParticleSystem.EmissionModule myEmission;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        GameMasterGo = GameObject.Find("GameMaster");
        GuiManager = GameMasterGo.GetComponent<guiManager>();
        GameSettings = GameMasterGo.GetComponent<game_settings>();
        GameObjectHandler = GameMasterGo.GetComponent<GameObject_handler>();
    }

    void Start()
    {
        currentThrusterFuel = MaxThrusterFuel;
        GuiManager.UpdateFuelText(currentThrusterFuel, MaxThrusterFuel);
        GuiManager.UpdateSpeedText(0f);

        myJetInst = (GameObject)Instantiate(GameObjectHandler.JetParticleEffect, new Vector3(transform.position.x, transform.position.y, GameObjectHandler.transform.position.z) - transform.right * 0.3f, Quaternion.Euler(-90f, 0f, 0f));
        myJetInst.transform.SetParent(this.transform);
        myEmission = myJetInst.GetComponent<ParticleSystem>().emission;
        myEmission.enabled = false;

    }

	void Update () {
		if (!GetComponent<health>().getIsDead()) {
            MouseRotation();
			Move();
		}

		if (GameSettings.playerWrapAroundScreen)
			wrapScreen();
	}

    private void MouseRotation()
    {
		Vector3 mousePosition = Input.mousePosition;
		Vector3 mouseLook = Camera.main.ScreenToWorldPoint(mousePosition);
		mouseLook = mouseLook - transform.position;
		float angle = Mathf.Atan2(mouseLook.y, mouseLook.x) * Mathf.Rad2Deg;

		if(instantTurning) {
        	transform.eulerAngles = new Vector3(0f, 0f, angle);
		} else {
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle)), Time.deltaTime * turningSpeed);
		}
    }

	// TODO:
	// 1. Acceleration
    private void Move()
    {
        int verticalMovement = 0;

        if (currentThrusterFuel < 0) currentThrusterFuel = 0f;

		// degen fuel
        if (Input.GetMouseButton(0) && currentThrusterFuel > 0f)
        {
            currentThrusterFuel -= Time.deltaTime * thrusterFuelDegenRate;
            verticalMovement = 1;

            myEmission.enabled = true;
        }
        else
        {
			verticalMovement = 0;
            myEmission.enabled = false;
        }

		// regen fuel 
		if(Input.GetMouseButton(0) == false) {
			if(currentThrusterFuel < MaxThrusterFuel) {
				currentThrusterFuel += Time.deltaTime * thrusterFuelRegenRate;
			}
		}

		// cap movement speed
        if (myBody.velocity.magnitude > maxMovementSpeed)
        {
            myBody.velocity = myBody.velocity.normalized * maxMovementSpeed;
        }
        else
        {
			// move the player.
			myBody.AddForce(transform.right * verticalMovement * movementSpeedMultiplier * 0.01f, ForceMode2D.Impulse);
        }

		// update GUI
		GuiManager.UpdateSpeedText(myBody.velocity.magnitude);
		GuiManager.UpdateFuelText(currentThrusterFuel, MaxThrusterFuel);
    }

    private void wrapScreen()
    {

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = screenRatio * Camera.main.orthographicSize;

        // top
        if (transform.position.y > Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, -Camera.main.orthographicSize, transform.position.z);
        }

        // bottom
        if (transform.position.y < -Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.z);
        }

        // right
        if (transform.position.x > widthOrtho)
        {
            transform.position = new Vector3(-widthOrtho, transform.position.y, transform.position.z);
        }

        // left
        if (transform.position.x < -widthOrtho)
        {
            transform.position = new Vector3(widthOrtho, transform.position.y, transform.position.z);
        }
    }
}
