using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerMovement player;

    public InventoryManagement inventory;

    public string[] inventoryItemData;
    
    public int[] potionStackIndex;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        inventory = GameObject.FindObjectOfType<InventoryManagement>();
        player = GameManager.FindObjectOfType<PlayerMovement>();

        LoadInventory();
    }

    public void LoadInventory()
    {
        inventory = GameObject.FindObjectOfType<InventoryManagement>();
        player = GameManager.FindObjectOfType<PlayerMovement>();

        //player.LoadPlayer();
        PlayerData data = SaveOutput.LoadPlayer();

        for (int i = 0; i < inventoryItemData.Length; i++)
        {
            Transform slot = inventory.inventorySlots[i].transform;
            
            //inventoryItemData[i] = data.items[i];
            //potionStackIndex[i] = data.itemStackIndex[i];

            inventory.AddAttackToInventory(slot, inventoryItemData[i]);

            inventory.AddWeaponToInventory(slot, inventoryItemData[i]);

            inventory.AddPotionToInventory(slot, inventoryItemData[i]);

            if (slot.childCount > 0)
            {
                GameObject child = slot.GetChild(0).gameObject;

                if (child.CompareTag("potion"))
                {
                    child.GetComponent<PotionManager>().itemStack = potionStackIndex[i];

                    child.GetComponentInChildren<TextMeshProUGUI>().text = potionStackIndex[i].ToString();
                }
            }

        }
    }
}
