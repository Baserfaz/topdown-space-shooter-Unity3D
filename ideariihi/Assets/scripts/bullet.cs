using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	[Header("Bullet colors")]
	public Color normalBulletColor = Color.grey;
	public Color lightningBulletColor = Color.blue;
	public Color fireBulletColor = Color.red;
	public Color iceBulletColor = Color.white;

    private GameObject GameMasterGo;
    private game_settings GameSettings;
	private GameObject myShadow;

	private attributes.DamageType bulletDamageType = attributes.DamageType.Normal;

    public float damage = 15f;
    public float timetolive = 5f;

    private float timer = 0f;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
        GameSettings = GameMasterGo.GetComponent<game_settings>();
		myShadow = transform.GetChild(0).gameObject;
    }

    void Start()
    {
        timer = timetolive + Time.time;

		switch(bulletDamageType) {
		case attributes.DamageType.Normal:
			ChangeColor(normalBulletColor);
			break;
		case attributes.DamageType.Lightning:
			ChangeColor(lightningBulletColor);
			break;
		case attributes.DamageType.Fire:
			ChangeColor(fireBulletColor);
			break;
		case attributes.DamageType.Ice:
			ChangeColor(iceBulletColor);	
			break;
		}
    }

    void Update()
    {
		if (timer < Time.time) {
			Destroy(myShadow.gameObject);
            Destroy(this.gameObject);
		}

        if (GameSettings.bulletsWrapAroundScreen)
            wrapScreen();
    }

	public void ChangeDamageType(attributes.DamageType d) {
		bulletDamageType = d;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.GetComponent<health>() != null) col.gameObject.GetComponent<health>().takeDamage(damage, bulletDamageType);
		Destroy(myShadow.gameObject);
		Destroy(this.gameObject);
	}

    public void setIsPlayers(bool b)
    {
        if (b) gameObject.layer = 11;
        else gameObject.layer = 10;
    }

	public void ChangeColor(Color c)
    {
		GetComponent<SpriteRenderer>().color = c;
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
