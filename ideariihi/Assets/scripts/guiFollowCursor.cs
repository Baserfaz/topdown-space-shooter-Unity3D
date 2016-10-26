using UnityEngine;
using System.Collections;

public class guiFollowCursor : MonoBehaviour {

    private GameObject playerGo;
    private GameObject GameMasterGo;

    public bool FollowPlayer = true;
    public GameObject cursor;
    private Vector3 offset = new Vector3(5f, 0f, 0f);

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
    }

	void Update () {

        playerGo = GameMasterGo.GetComponent<GameObject_handler>().getPlayer();

        if(FollowPlayer && playerGo != null)
            transform.position = Camera.main.WorldToScreenPoint(playerGo.transform.position + offset);
        else
            transform.position = Camera.main.WorldToScreenPoint(cursor.transform.position + offset);
	}
}
