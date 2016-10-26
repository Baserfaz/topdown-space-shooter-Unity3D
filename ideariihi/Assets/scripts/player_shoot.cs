using UnityEngine;
using System.Collections;

public class player_shoot : MonoBehaviour {

    private GameObject GameMasterGo;
    private GameObject bulletPrefab;

	private attributes.DamageType playerDamageType = attributes.DamageType.Normal;

	private float shootTimer = 0f;


	public Color playerBulletColor = Color.cyan;
	public float fireRate = 0.1f;
    public float BulletMovementSpeed = 5f;
    public float spreadAmount = 0.1f;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
        bulletPrefab = GameMasterGo.GetComponent<GameObject_handler>().bulletPrefab;
    }

	void Update () {
        if (Input.GetMouseButton(1) && shootTimer < Time.time && !GetComponent<health>().getIsDead())
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        Vector3 bulletOffset = transform.right * 0.5f;

        GameObject bulletInst = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, bulletPrefab.transform.position.z) + bulletOffset, Quaternion.identity);

        Vector3 spreadVector = new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount), 0f);

        bulletInst.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletMovementSpeed + spreadVector, ForceMode2D.Impulse);

		bullet bulletScript = bulletInst.GetComponent<bullet>();

		bulletScript.setIsPlayers(true);
		bulletScript.ChangeColor(playerBulletColor);
		bulletScript.ChangeDamageType(playerDamageType);

        shootTimer = fireRate + Time.time;
    }
}
