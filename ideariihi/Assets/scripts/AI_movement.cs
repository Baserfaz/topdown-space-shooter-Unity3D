using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour {

    private attributes myAttributes;

    private Rigidbody2D myBody;
    private GameObject GameMasterGo;
    private GameObject playerGo;
    private game_settings GameSettings;
    private health myHealth;

	private float movementTimer = 0f;
	private float timeBetweenMovementBursts = 0.2f;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
        myBody = GetComponent<Rigidbody2D>();
        GameSettings = GameMasterGo.GetComponent<game_settings>();
        myHealth = GetComponent<health>();
        myAttributes = GetComponent<attributes>();
    }

	void Start () {
        playerGo = GameMasterGo.GetComponent<GameObject_handler>().getPlayer();
		myBody.mass = myAttributes.movementControl;
	}

	void Update () {
        if (playerGo != null)
        {
			if (myHealth.getIsDead() == false) {
                RotateTowardsPlayer();
				MoveTowardsPlayerImpulse();
			}
        }

		if (GameSettings.enemiesWrapAroundScreen)
			wrapScreen();

	}

    private void RotateTowardsPlayer()
    {
        Vector3 playerPosition = playerGo.transform.position;
        playerPosition = playerPosition - transform.position;
        float angle = Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

	private void MoveTowardsPlayerImpulse()
	{
		if(movementTimer < Time.time) {
			myBody.AddForce(transform.right * myAttributes.movementSpeed * 0.1f, ForceMode2D.Impulse);
			movementTimer = Time.time + timeBetweenMovementBursts;
		}
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
