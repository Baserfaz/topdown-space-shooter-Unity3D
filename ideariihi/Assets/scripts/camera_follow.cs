using UnityEngine;
using System.Collections;

public class camera_follow : MonoBehaviour {

    private GameObject playerGo;
    private GameObject GameMasterGo;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
    }

	void Update () {

        if (playerGo != null)
        {
            Vector3 targetPos = new Vector3(playerGo.transform.position.x, playerGo.transform.position.y, Camera.main.transform.position.z);
            transform.position = targetPos;
        }
        else
        {
            playerGo = GameMasterGo.GetComponent<GameObject_handler>().getPlayer();
        }

	}
}
