using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerMovement player;
    
    public Slider playerSlider;

    public HotbarManagement hotBar;

    public InventoryManagement inventory;

    public InputActionAsset actions;

    [HideInInspector]
    public InputAction pause;

    public GameObject pauseMenu;

    public GameObject gameOverScreen;

    public bool canPause = true;

    public bool isPaused = false;

    private bool waitForReset = false;

    public string[] inventoryItemData;
    
    public int[] potionStackIndex;

    private Animator gameOverScreenAnim;

    [SerializeField]
    public Dictionary<string, bool> objectStates = new Dictionary<string, bool>();

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        
    }

    private void Start() 
    {

        CheckForPlayerInstance();
        
        pause = actions.FindActionMap("Gameplay").FindAction("pause");
        pause.performed += PauseGame;

        pauseMenu.SetActive(false);

        gameOverScreenAnim = gameOverScreen.GetComponent<Animator>();
        gameOverScreen.SetActive(false);

        UpdateObjectStates();

        Scene firstScene = SceneManager.GetSceneByBuildIndex(1);

        if (firstScene.isLoaded && SceneManager.GetActiveScene() != firstScene)
        {
            SceneManager.UnloadSceneAsync("test_dungeon_1F");
            SceneManager.UnloadSceneAsync("test_dungeon_2F");
        }

        //LoadInventory();
    }

    private void LateUpdate()
    {
        if (playerSlider && playerSlider.value == 0)
        {
            gameOverScreen.SetActive(true);
            if (!waitForReset)
            {
                gameOverScreenAnim.SetTrigger("gameOverTrigger");
                StartCoroutine(WaitToResetLevelAfterDeath());
            }
        }
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("test_dungeon_1F");

        if (SceneManager.GetActiveScene().isLoaded)
        {
            inventoryItemData = new string[48];
            potionStackIndex = new int[48];
            inventoryItemData[40] = "fireball";
        
            StartCoroutine(ReloadInventory());
            
        }
    }

    public void ContinueGame()
    {

        SceneManager.LoadScene("test_dungeon_1F");

        if (SceneManager.GetActiveScene().isLoaded)
        {

            StartCoroutine(ReloadInventory());
            
        }
    }

    public void QuitGameMain()
    {
        Application.Quit();
    }

    public void CheckForPlayerInstance()
    {
        if (GameObject.FindObjectOfType<PlayerMovement>())
        {
            playerSlider = GameObject.FindGameObjectWithTag("playerHealth").GetComponent<Slider>();
            hotBar = GameObject.FindGameObjectWithTag("hotBar").GetComponent<HotbarManagement>();
            inventory = GameObject.FindGameObjectWithTag("inventory").GetComponent<InventoryManagement>();
        }
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
            
            inventoryItemData[i] = data.items[i];
            potionStackIndex[i] = data.itemStackIndex[i];

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

    IEnumerator ReloadInventory()
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

    IEnumerator WaitToResetLevelAfterDeath()
    {
        canPause = false;
        waitForReset = true;
        player.enabled = false;

        SaveGame();

        yield return new WaitForSeconds(9);

        Destroy(player.gameObject);

        playerSlider.value = 100;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().isLoaded)
        {
            CheckForPlayerInstance();


            /*for (int i = 0; i < inventory.inventorySlots.Length; i++)
            {
                Transform slot = inventory.inventorySlots[i].transform;


                if(slot.childCount > 0)
                {
                    GameObject child = slot.GetChild(0).gameObject;
                    Destroy(child);
                }
            }*/
        }

        waitForReset = false;
        gameOverScreen.SetActive(false);
        
        yield return new WaitForSeconds(1);

        if (player == null)
        {
            player = GameManager.FindObjectOfType<PlayerMovement>();
        }

        UpdateObjectStates();

        player.enabled = false;

        canPause = true;
        
        player.enabled = true;
    }

    void PauseGame(InputAction.CallbackContext ctx)
    {
        if (canPause)
        {
            if (ctx.performed)
            {
                isPaused = !isPaused;
            }

            if (isPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        GameManager manager = GameObject.FindObjectOfType<GameManager>();

        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            Transform slot = inventory.inventorySlots[i].transform;

            if (slot.childCount > 0)
            {
                GameObject child = slot.GetChild(0).gameObject;

                manager.inventoryItemData[i] = child.name;

                //Debug.Log(child.name);

                if (child.CompareTag("potion"))
                {
                    int potionStackCount = child.GetComponent<PotionManager>().itemStack;

                    manager.potionStackIndex[i] = potionStackCount;
                }
            }
            else
            {
                manager.inventoryItemData[i] = "";
            }
        }

        //player.SavePlayer();
        
        Debug.Log("GameSaved");
        SaveOutput.SavePlayer(manager);
    }

    void UpdateObjectStates()
    {
        GameObject[] chestObjects = GameObject.FindGameObjectsWithTag("normalChest");
        GameObject[] doorObjects = GameObject.FindGameObjectsWithTag("door");

        PlayerData data = SaveOutput.LoadPlayer();

        objectStates = data.objectStates;

        for (int i = 0; i < chestObjects.Length; i++)
        {

            if (chestObjects[i].GetComponent<Chest>().isOpen)
            {
                int objectInSceneID = chestObjects[i].GetComponent<Chest>().chestID;
                
                foreach (var obj in objectStates)
                {

                    if (chestObjects[i].name == obj.Key)
                    {
                        chestObjects[i].GetComponent<Chest>().isOpen = true;
                        chestObjects[i].GetComponent<Chest>().ChestIsOpened();
                    }

                    //Debug.Log("Object name: " + obj.Key + " Object state: " + obj.Value);
                    
                }
            }
        }

        for (int i = 0; i < doorObjects.Length; i++)
        {
            if (doorObjects[i].GetComponent<Door>().isOpen)
            {
                
                foreach (var obj in objectStates)
                {

                    if (doorObjects[i].name == obj.Key)
                    {
                        doorObjects[i].GetComponent<Door>().isOpen = true;
                    }

                    //Debug.Log("Object name: " + obj.Key + " Object state: " + obj.Value);
                    
                }
            }
        }
    }
    
    public void QuitGamePause()
    {
        Application.Quit();
    }
}
