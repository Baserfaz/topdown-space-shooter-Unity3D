using UnityEngine;
using System.Collections;

public class AI_shoot : MonoBehaviour {

    private attributes myAttributes;
    private GameObject bulletGo;
    private GameObject target;
    private GameObject GameMasterGo;
    private health myHealth;

    public float bulletForce = 10f;
    private float timer = 0f;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
        target = GameMasterGo.GetComponent<GameObject_handler>().getPlayer();
        bulletGo = GameMasterGo.GetComponent<GameObject_handler>().bulletPrefab;
        myHealth = GetComponent<health>();
        myAttributes = GetComponent<attributes>();
    }

	void Update () {
        if (myHealth.getIsDead() == false)
        {
            if (timer < Time.time)
            {
                timer = Time.time + myAttributes.fireRate;
                Shoot();
            }
        }
	}

    private void Shoot()
    {
        if (target != null)
        {
            GameObject bulletInst = (GameObject)Instantiate(bulletGo, transform.position, Quaternion.identity);
			bullet bulletScript = bulletInst.GetComponent<bullet>();

			bulletScript.damage = myAttributes.damage;
            bulletInst.transform.localScale = new Vector3(myAttributes.shotSize, myAttributes.shotSize, bulletInst.transform.localScale.z);

			bulletScript.ChangeDamageType(myAttributes.myDamageType);

            Rigidbody2D bulletRB = bulletInst.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(transform.right * myAttributes.bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
