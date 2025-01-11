using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public static InventoryManagement instance;

    public HotbarManagement hotbarManagement;

    public InventoryObjects inventoryObjects;

    public GameObject[] inventorySlots;

    public bool canSelectTarget = false;

    public string objectSelected;

    private int keyStack = 1;

    public bool canStack;

    public bool canTalk = false;

    private int[] avalibeSlots;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        avalibeSlots = new int[inventorySlots.Length];
    }

    private void Update()
    {
        if (canSelectTarget != true)
        {
            MouseSelector();
        }
    }

    private void FixedUpdate()
    {
        CheckSlots();
    }

    private void CheckSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount > 0)
            {
                avalibeSlots[i] = 1;
            }
            else
            {
                avalibeSlots[i] = 0;
            }
        }
    }

    public void MouseSelector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Collectable"))
                {
                    PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();

                    float distance = Vector3.Distance(hit.collider.gameObject.transform.position, player.transform.position);

                    if (distance < 10)
                    {
                        objectSelected = hit.collider.tag;

                        ManageInventory(objectSelected, hit);
                    }
                    else
                    {
                        Debug.Log("Too Far Away!");
                    }
                }

                if (canTalk == true && hit.collider.CompareTag("NPC"))
                {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue();
                }

                if (hit.collider.CompareTag("lever"))
                {
                    GameObject lever = hit.collider.gameObject;

                    DoorManager doorManager = FindObjectOfType<DoorManager>();

                    if (!lever.GetComponent<LeverPullCheck>().isPulled == true)
                    {
                        lever.GetComponent<LeverPullCheck>().isPulled = true;

                        doorManager.CheckPullCount += 1;
                    }
                    else
                    {
                        lever.GetComponent<LeverPullCheck>().isPulled = false;

                        doorManager.CheckPullCount -= 1;
                    }
                    
                }

                if (hit.collider.CompareTag("normalChest"))
                {
                    GameObject chest = hit.collider.gameObject;

                    if (!chest.GetComponent<Chest>().isOpen)
                    {
                        chest.GetComponent<Chest>().OpenChest();
                        chest.GetComponent<Chest>().isOpen = true;
                    }
                }
            }
        }
        
    }

    private void ManageInventory(string objectSelected, RaycastHit hit)
    {
        CheckForStackableItem(objectSelected, hit);

        AddItemToSlot(objectSelected, hit);
    }

    public void CheckForStackableItem(string objectSelected, RaycastHit hit)
    {
        InventoryCheck(objectSelected, hit);
    }

    public void InventoryCheck(string objectSelected, RaycastHit hit)
    {
        //HotbarCheck(objectSelected, hit);

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            Transform slot = inventorySlots[i].transform;

            if (slot.childCount > 0)
            {
                GameObject child = slot.GetChild(0).gameObject;

                if (objectSelected == "potion" && child.CompareTag("potion"))
                {

                    PotionManager potionManagerHolder = child.GetComponent<PotionManager>();

                    string potionName = hit.collider.GetComponent<PotionHolder>().potion.potionName;

                    //Debug.Log("Check Runned" + potionName);

                    if (potionName == potionManagerHolder.potionName)
                    {
                        //Debug.Log("can stack");

                        canStack = true;

                        UpdatePotionStack(child, potionManagerHolder, hit);

                        break;
                    }
                    else
                    {
                        canStack = false;
                    }

                }

                if (objectSelected == "key" && child.CompareTag("key"))
                {
                    canStack = true;
                    UpdateKeyStack(child, hit);

                    break;
                }
                else
                {
                    canStack = false;
                }

                if (objectSelected == "door" && child.CompareTag("key"))
                {
                    DoorManager isKeyCheck = GameObject.FindObjectOfType<DoorManager>();

                    if (isKeyCheck.isKeyOnly == true)
                    {
                        hit.collider.GetComponent<BoxCollider>().enabled = false;

                        Door doorManager = GameObject.FindObjectOfType<Door>();

                        doorManager.isOpen = true;

                        TextMeshProUGUI textMeshPro = child.GetComponentInChildren<TextMeshProUGUI>();

                        if (keyStack > 1)
                        {
                            keyStack--;
                            textMeshPro.text = keyStack.ToString();
                        }
                        else
                        {
                            Destroy(child);
                        }
                    }
                    else
                    {
                        return;
                    }
                    
                    
                }
            }
            
        }

        
    }

    private void UpdatePotionStack(GameObject child, PotionManager potionManager, RaycastHit hit)
    {
        TextMeshProUGUI textMeshPro = child.GetComponentInChildren<TextMeshProUGUI>();

        if (potionManager.itemStack != 99)
        {
            potionManager.itemStack++;
            textMeshPro.text = potionManager.itemStack.ToString();
            Destroy(hit.collider.gameObject);
        }
        else
        {
            textMeshPro.text = potionManager.itemStack.ToString();
        }
    }

    private void UpdateKeyStack(GameObject child, RaycastHit hit)
    {
        TextMeshProUGUI textMeshPro = child.GetComponentInChildren<TextMeshProUGUI>();

        if (keyStack != 99)
        {
            keyStack++;
            textMeshPro.text = keyStack.ToString();
            Destroy(hit.collider.gameObject);
        }
        else
        {
            textMeshPro.text = keyStack.ToString();
        }
    }

    public void AddItemToSlot(string objectSelected, RaycastHit hit)
    {
        SpellBookHolder spellBookHolder = hit.collider.GetComponent<SpellBookHolder>();

        PotionHolder potionHolder = hit.collider.GetComponent<PotionHolder>();

        WeaponHolder weaponHolder = hit.collider.GetComponent<WeaponHolder>();

        KeyHolder keyHolder = hit.collider.GetComponent<KeyHolder>();

        for (int i = 0; i < avalibeSlots.Length; i++)
        {
            if (avalibeSlots[i] != 1)
            {
                switch (objectSelected)
                {
                    case "book":

                        AddAttackToInventory(inventorySlots[i].transform, spellBookHolder.SpellBook.bookID);
                        Destroy(hit.collider.gameObject);
                        break;

                    case "potion":

                        if (!canStack)
                        {
                            AddPotionToInventory(inventorySlots[i].transform, potionHolder.potion.potionID);
                            Destroy(hit.collider.gameObject);
                            break;
                        }

                        break;

                    case "weapon":

                        AddWeaponToInventory(inventorySlots[i].transform, weaponHolder.weapon.weaponID);
                        Destroy(hit.collider.gameObject);
                        break;

                    case "key":
                    
                        if (!canStack)
                        {
                            AddKeyToInventory(inventorySlots[i].transform, keyHolder.key.keyID);
                            Destroy(hit.collider.gameObject);
                        }
                        break;
                }

                break;
            }
        }
    }

    // Method to add an item to a specific slot
    public void AddAttackToInventory(Transform slotTransform, string book_ID)
    {
        for (int i = 0; i < inventoryObjects.attacks.Length; i++)
        {
            if (book_ID == inventoryObjects.attacks[i].name)
            {

                Button newItem = Instantiate(inventoryObjects.attacks[i], slotTransform);

                newItem.name = book_ID;

                newItem.transform.localPosition = Vector3.zero;

                //Debug.Log("Added item: " + newItem.name + " to slot: " + slotTransform.name);
            }
        }
        
    }

    public void AddPotionToInventory(Transform slotTransform, string potion_ID)
    {

        for (int i = 0; i < inventoryObjects.potions.Length; i++)
        {
            //Debug.Log(potion_ID + " | " + inventoryObjects.potions[i].name);

            if (potion_ID == inventoryObjects.potions[i].name)
            {
                Button newItem = Instantiate(inventoryObjects.potions[i], slotTransform);

                newItem.name = potion_ID;

                newItem.transform.localPosition = Vector3.zero;

                //Debug.Log("Added item: " + newItem.name + " to slot: " + slotTransform.name);
            }
        }
    }

    public void AddWeaponToInventory(Transform slotTransform, string weapon_ID)
    {
        //Debug.Log("I made it here");

        for (int i = 0; i < inventoryObjects.weapons.Length; i++)
        {
            if (weapon_ID == inventoryObjects.weapons[i].name)
            {
                Button newItem = Instantiate(inventoryObjects.weapons[i], slotTransform);

                newItem.name = weapon_ID;

                newItem.transform.localPosition = Vector3.zero;

                //Debug.Log("Added item: " + newItem.name + " to slot: " + slotTransform.name);
            }
        }
    }

    public void AddKeyToInventory(Transform slotTransform, string key_ID)
    {
        for (int i  = 0; i < inventoryObjects.keys.Length; i++)
        {
            if (key_ID == inventoryObjects.keys[i].name)
            {
                Button newItem = Instantiate(inventoryObjects.keys[i], slotTransform);

                newItem.name = key_ID;

                newItem.transform.localPosition = Vector3.zero;
            }
        }
    }
}
