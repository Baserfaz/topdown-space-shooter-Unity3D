using UnityEngine;
using System.Collections;

public class camera_background_color_changer : MonoBehaviour {

	public float timeGapBetweenColors = 2f;
	public float colorChangeSpeed = 5f;
	private float timer = 0f;

	void Update() {
		if(timer < Time.time) {
			timer = Time.time + colorChangeSpeed + timeGapBetweenColors;
			StartCoroutine("ChangeColor");
		}
	}

	private IEnumerator ChangeColor() {
		float currentTime = 0f;
		Camera mycam = GetComponent<Camera>();
		Color endColor = new Color(Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), 1f);

		while(currentTime < colorChangeSpeed) {
			currentTime += Time.deltaTime;
			mycam.backgroundColor = Color.Lerp(mycam.backgroundColor, endColor, currentTime / colorChangeSpeed);
			yield return null;
		}
	}
}
