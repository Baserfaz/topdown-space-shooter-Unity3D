using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {

    private GameObject playerGo;
    private GameObject GameMasterGo;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
    }

	public void enableCrosshair() {
		this.gameObject.SetActive(true);
		Cursor.visible = false;
	}

	void Update () {



		if(playerGo == null) playerGo = GameMasterGo.GetComponent<GameObject_handler>().getPlayer();
		else {
        	Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        	Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        	transform.position = pos;
        	transform.rotation = playerGo.transform.rotation * Quaternion.Euler(new Vector3(0f, 0f, -90f));
		}
	}
}
