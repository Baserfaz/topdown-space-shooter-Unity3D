using UnityEngine;
using System.Collections;

public class shadow_controller : MonoBehaviour {

	private GameObject shadowGo;
	private Vector3 offset = new Vector3(0.2f, -0.3f, 0f);

	private health myHealth;

	void Awake() {
		shadowGo = transform.FindChild("shadow").gameObject;
		myHealth = GetComponent<health>();
	}

	void Start() {
		shadowGo.transform.SetParent(null);
	}

	void Update () {

		if(myHealth != null) {
			if(myHealth.getIsDead() == false) {
				shadowGo.transform.position = transform.position + offset;
				shadowGo.transform.rotation = transform.rotation;
			} else {
				Destroy(shadowGo);
				this.enabled = false;
			}
		} else {
			shadowGo.transform.position = transform.position + offset;
			shadowGo.transform.rotation = transform.rotation;
		}

	}
}
