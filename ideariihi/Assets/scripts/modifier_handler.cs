using UnityEngine;
using System.Collections.Generic;

// this class creates modifications
public class modifier_handler : MonoBehaviour {

    private List<modifier> prefixes = new List<modifier>();
    private List<modifier> suffixes = new List<modifier>();

    [Range(0f, 100f)]
    public float prefixChance = 50f;
    [Range(0f, 100f)]
    public float suffixChance = 75f;

    void Awake()
    {

		// damage types:
		// 0. normal
		// 1. lightning
		// 2. ice
		// 3. fire

		// prefixes
        modifier pm1 = new modifier("Large", new string[] { "size", "movSpeed", "damage", "fireRate", "shotSize" }, new float[] { 1.2f, -2f, 5f, 0.5f, 3f }, 1);
        prefixes.Add(pm1);

        modifier pm2 = new modifier("Quick", new string[] { "size", "movSpeed", "fireRate" }, new float[] { -0.25f, 10f, -0.15f}, 2);
        prefixes.Add(pm2);

		modifier pm3 = new modifier("Lightning", new string[] { "damageType", "lightningRes" }, new float[] { 1, 50 }, 2);
		prefixes.Add(pm3);

		modifier pm4 = new modifier("Fiery", new string[] { "damageType", "fireRes" }, new float[] { 3, 50 }, 2);
		prefixes.Add(pm4);

		modifier pm5 = new modifier("Icy", new string[] { "damageType", "iceRes" }, new float[] { 2, 50 }, 2);
		prefixes.Add(pm5);

		modifier pm6 = new modifier("Armored", new string[] { "armor" }, new float[] { 50 }, 2);
		prefixes.Add(pm6);

		modifier pm7 = new modifier("Resistant", new string[] { "armor", "iceRes", "fireRes", "lightningRes"  }, new float[] { 10, 10, 10, 10 }, 2);
		prefixes.Add(pm7);

		// suffixes
        modifier sm1 = new modifier("Of Stone", new string[] { "movSpeed", "damage" }, new float[] { -2f, 10f}, 1);
        suffixes.Add(sm1);

		modifier sm2 = new modifier("Of Swiftness", new string[] {"movSpeed", "movControl"}, new float[] { 5f, -0.05f }, 1);
		suffixes.Add(sm2);
    }

    public modifier GetRandomSuffix()
    {
        if (suffixes.Count == 0) return null;
        float randomNum = Random.Range(0f, 100f);
        if (randomNum > suffixChance) return suffixes[Random.Range(0, suffixes.Count)];
        return null;
    }

    public modifier GetRandomPrefix()
    {
        if (prefixes.Count == 0) return null;
        float randomNum = Random.Range(0f, 100f);
        if (randomNum > prefixChance) return prefixes[Random.Range(0, prefixes.Count)];
        return null;
    }


}
