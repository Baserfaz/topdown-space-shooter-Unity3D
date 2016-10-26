using UnityEngine;
using System.Collections.Generic;

public class attributes : MonoBehaviour {

    private GameObject GameMasterGo;
    private modifier_handler ModifierHandler;
	private resistances myResistances;

    public string name;
    public string prefixName = "";
    public string suffixName = "";

    public string fullName = "";

    public float movementSpeed = 1f;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10f;
    public float size = 1f;
    public float damage = 1f;
    public float shotSize = 2f;
	public float movementControl = 1f;

	public enum DamageType { Normal, Fire, Ice, Lightning }
	public DamageType myDamageType = DamageType.Normal;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
        ModifierHandler = GameMasterGo.GetComponent<modifier_handler>();
		myResistances = GetComponent<resistances>();
    }

    // get a random modifiers and apply them.
    void Start()
    {
        modifier prefix = ModifierHandler.GetRandomPrefix();
        modifier suffix = ModifierHandler.GetRandomSuffix();

        // handle prefix
        if (prefix != null) {
            prefixName = prefix.modName;
            foreach (KeyValuePair<string, float> pair in prefix.attributeToAmount) {
				checkModifiers(pair);
            }
        }

        // handle suffix
        if(suffix != null) {
            suffixName = suffix.modName;
            foreach (KeyValuePair<string, float> pair in suffix.attributeToAmount) {
				checkModifiers(pair);
            }
        }
        
        fullName = prefixName + " " + name + " " + suffixName;

        this.transform.localScale = new Vector3(size, size, transform.localScale.z);
    }

	private void checkModifiers(KeyValuePair<string, float> pair) {
		if (pair.Value == null) return;

		if(pair.Key.Equals("damageType")) {
			if(pair.Value == 1) myDamageType = DamageType.Lightning;
			if(pair.Value == 2) myDamageType = DamageType.Ice;
			if(pair.Value == 3) myDamageType = DamageType.Fire;
			return;
		}

		if(pair.Key.Equals("movControl")) {
			movementControl += pair.Value;
			return;
		}

		if(pair.Key.Equals("armor")) {
			myResistances.setArmor(pair.Value);
			return;
		}

		if(pair.Key.Equals("lightningRes")) {
			myResistances.setLightningResistance(pair.Value);
			return;
		}

		if(pair.Key.Equals("fireRes")) {
			myResistances.setFireResistance(pair.Value);
			return;
		}

		if(pair.Key.Equals("iceRes")) {
			myResistances.setIceResistance(pair.Value);
			return;
		}

		if (pair.Key.Equals("movSpeed"))
		{
			movementSpeed += pair.Value;
			return;
		}

		if (pair.Key.Equals("shotSize"))
		{
			shotSize += pair.Value;
			return;
		}

		if (pair.Key.Equals("damage"))
		{
			damage += pair.Value;
			return;
		}

		if (pair.Key.Equals("size"))
		{
			size += pair.Value;
			return;
		}

		if (pair.Key.Equals("fireRate"))
		{
			fireRate += pair.Value;
			if (fireRate <= 0f) fireRate = 0.5f;
			return;
		}
	}

}
