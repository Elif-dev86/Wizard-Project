using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Potions/Potion")]
public class Potion : ScriptableObject
{
    public string potionID;

    public string potionName;

    public string potionType;

    public int effectAmount;
}
