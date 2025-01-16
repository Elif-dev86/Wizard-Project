using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveStation : MonoBehaviour
{
    public InputActionAsset actions;

    private InputAction clickAction;

    private PlayerMovement player;

    public InventoryManagement inventory;

    private GameManager manager;
    public GameObject saveMenu;
    
    public bool canInteract;

    private void Awake() 
    {
        clickAction = actions.FindActionMap("Gameplay").FindAction("pointClick");

        clickAction.performed += OnClickPerformed;    
    }

    private void Start() 
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.CompareTag("saveStation") && !canInteract)
            {
                Debug.Log("Too Far Away!");
            }
            else if (hit.collider.CompareTag("saveStation") && canInteract)
            {
                saveMenu.transform.gameObject.SetActive(true);
            }
        }

    }

    public void SaveGame()
    {
        inventory = GameObject.FindObjectOfType<InventoryManagement>();
        player = GameManager.FindObjectOfType<PlayerMovement>();

        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            Transform slot = inventory.inventorySlots[i].transform;

            if (slot.childCount > 0)
            {
                GameObject child = slot.GetChild(0).gameObject;

                manager.inventoryItemData[i] = child.name;

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

        SaveOutput.SavePlayer(manager);
    }

    public void Resume()
    {
        saveMenu.transform.gameObject.SetActive(false);
    }
}
