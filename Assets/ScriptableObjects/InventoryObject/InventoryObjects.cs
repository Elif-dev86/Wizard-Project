using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventoryObjects/Objects")]
public class InventoryObjects : ScriptableObject
{
    public Button[] attacks;

    public Button[] potions;

    public Button[] weapons;

    public Button[] keys;
}
