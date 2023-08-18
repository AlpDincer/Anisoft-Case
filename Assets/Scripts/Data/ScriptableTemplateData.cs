using Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class ScriptableTemplateData : ScriptableObject
{
    public List<Vegetable> VegetableList;
    public List<Table> TableList;

    public int currentCoin;

    public bool isPlayed = false;
}
