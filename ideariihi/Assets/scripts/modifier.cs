using UnityEngine;
using System.Collections.Generic;

public class modifier {

    public string modName = "";
    public Dictionary<string, float> attributeToAmount = new Dictionary<string, float>();
    public int rarity = 0;

    public modifier(string name, string[] attributes, float[] amounts, int _rarity)
    {
        modName = name;

        for(int i = 0; i < attributes.Length; i++) 
        {
            attributeToAmount.Add(attributes[i], amounts[i]);
        }

        rarity = _rarity;
    }

}
