using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerMovement player;
    
    public Slider playerSlider;

    public HotbarManagement hotBar;

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
        

        //CheckForPlayerInstance();

        LoadInventory();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("test_game");

        if (SceneManager.GetActiveScene().isLoaded)
        {
            inventoryItemData = new string[43];
            potionStackIndex = new int[43];
            inventoryItemData[35] = "fireball";

            StartCoroutine(WaitBitch());
            
        }
    }

    public void ContinueGame()
    {

        SceneManager.LoadScene("test_game");

        if (SceneManager.GetActiveScene().isLoaded)
        {

            StartCoroutine(WaitBitch());
            
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CheckForPlayerInstance()
    {
        playerSlider = GameObject.FindGameObjectWithTag("playerHealth").GetComponent<Slider>();
        hotBar = GameObject.FindGameObjectWithTag("HotbarSlot").GetComponent<HotbarManagement>();
        inventory = GameObject.FindGameObjectWithTag("inventorySlot").GetComponent<InventoryManagement>();
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

    IEnumerator WaitBitch()
    {
        yield return new WaitForSeconds(1);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (player != null)
        {
            CheckForPlayerInstance();
            LoadInventory();
            Debug.Log("Is Loaded");
        }

        
    }
}
