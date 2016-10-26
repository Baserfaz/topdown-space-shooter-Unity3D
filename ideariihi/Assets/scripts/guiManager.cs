using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class guiManager : MonoBehaviour {

	[Header("Main GUI canvas")]
	public GameObject GuiCanvasGo;

	[Header("Gui-elements")]
    public GameObject guiFuelTextGo;
    public GameObject guiHealthTextGo;
    public GameObject guiSpeedTextGo;
	public GameObject guiCursorGo;
	public GameObject guiMainMenuGo;

	[Header("Gui-prefabs")]
	public GameObject guiFloatingDamageTextPrefab;

    public void UpdateFuelText(float fuelAmount, float maxFuel) {
        if (fuelAmount <= 0f) 
			guiFuelTextGo.GetComponent<Text>().text = "NO FUEL!";
        else
            guiFuelTextGo.GetComponent<Text>().text = "FUEL " + (fuelAmount / maxFuel * 100f).ToString("F2") + "%";
    }

    public void UpdateHealthText(float healthAmount, float maxHealth) {
        if (healthAmount <= 0f)
        	guiHealthTextGo.GetComponent<Text>().text = "DECEASED!";
        else
            guiHealthTextGo.GetComponent<Text>().text = "HEALTH " + (healthAmount / maxHealth * 100f).ToString("F2") + "%";
    }

    public void UpdateSpeedText(float currentSpeed) {
        guiSpeedTextGo.GetComponent<Text>().text = "SPEED " + currentSpeed.ToString("F2") + "u";
    }

	public void enableGuiCursor() {
		guiCursorGo.SetActive(true);
	}

	public void disablMainMenuGo() {
		guiMainMenuGo.SetActive(false);
	}

	public void ResetButton() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Cursor.visible = true;
	}

}
