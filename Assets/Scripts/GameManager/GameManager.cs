using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InventoryManagement inventory;

    public string[] inventoryItemData;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        inventory = GameObject.FindObjectOfType<InventoryManagement>();

        //SaveInventory();

        LoadInventory();
    }

    void Update()
    {
        
    }

    public void SaveInventory()
    {
        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            Transform slot = inventory.inventorySlots[i].transform;

            if (slot.childCount > 0)
            {
                GameObject child = slot.GetChild(0).gameObject;

                inventoryItemData[i] = child.name;
            }
            else
            {
                inventoryItemData[i] = "";
            }
        }
    }

    public void LoadInventory()
    {
        for (int i = 0; i < inventoryItemData.Length; i++)
        {
            Transform slot = inventory.inventorySlots[i].transform;

            inventory.AddAttackToInventory(slot, inventoryItemData[i]);
        }
    }
}
