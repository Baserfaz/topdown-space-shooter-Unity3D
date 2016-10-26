using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gui_scale_background_to_text : MonoBehaviour {
	
	void Start () {

		int length = GetComponent<Text>().text.Length;
		int fontSize = GetComponent<Text>().fontSize;

		GetComponent<RectTransform>().sizeDelta = new Vector2(length * fontSize - fontSize * 2, 35f);
	}
}
