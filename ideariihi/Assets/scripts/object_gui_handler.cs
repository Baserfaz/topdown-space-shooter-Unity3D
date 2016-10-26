using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class object_gui_handler : MonoBehaviour {

	private GameObject GameMasterGo;
	private GameObject GUIGo;
	private GameObject NamePlateInst;

	private health myHealth;

	public GameObject NamePlatePrefab;
	public Vector3 offset = Vector3.zero;

	private Vector3 totalOffset = Vector3.zero;
	private Text myText;


	void Awake() {
		GameMasterGo = GameObject.Find("GameMaster");
		GUIGo = GameMasterGo.GetComponent<guiManager>().GuiCanvasGo;
		myHealth = GetComponent<health>();
	}

	void Start() {
		if(NamePlatePrefab != null) {
			NamePlateInst = (GameObject)Instantiate(NamePlatePrefab, transform.position, Quaternion.identity);
			myText = NamePlateInst.GetComponentInChildren<Text>();
			NamePlateInst.transform.SetParent(GUIGo.transform);
		}
		totalOffset = offset * GetComponent<attributes>().size;
	}

	void Update() {

		// set the name here, because sometimes start methods run in a wrong order and turns name empty.
		if(myText.text.Equals("")) myText.text = GetComponent<attributes>().fullName.ToUpper();

		if(myHealth.getIsDead()) {
			Destroy(NamePlateInst.gameObject);
			this.enabled = false;
		}
		NamePlateInst.transform.position = Camera.main.WorldToScreenPoint(transform.position + totalOffset);
	}

}
