using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponID;

    public string weaponName;

    public string weaponType;

    public int damangeAmount;
}
