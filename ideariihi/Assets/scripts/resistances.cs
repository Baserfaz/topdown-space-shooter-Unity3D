using UnityEngine;
using System.Collections;

public class resistances : MonoBehaviour {

	// 0% to 100% dampen received damage.

	// Resistances against elemental damage
	private float fireResistance = 0f;
	private float iceResistance = 0f;
	private float lightningResistance = 0f;

	// Against normal bullets
	private float armor = 0f; 

	// setters
	public void setIceResistance(float r) {
		if(r > 0f && r < 100f) iceResistance = r;
	}

	public void setLightningResistance(float r) {
		if(r > 0f && r < 100f) lightningResistance = r;
	}

	public void setFireResistance(float r) {
		if(r > 0f && r < 100f) fireResistance = r;
	}

	public void setArmor(float r) {
		if(r > 0f && r < 100f) armor = r;
	}

	public void setAllResistances(float ice, float lightning, float fire, float armor) {
		setIceResistance(ice);
		setLightningResistance(lightning);
		setFireResistance(fire);
		setArmor(armor);
	}

	// getters
	public float getIceResistance() {
		return iceResistance;
	}

	public float getFireResistance() {
		return fireResistance;
	}

	public float getLightningResistance() {
		return lightningResistance;
	}

	public float getArmor() {
		return armor;
	}

}
