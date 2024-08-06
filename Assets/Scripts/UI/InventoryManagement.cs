using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public HotbarManagement hotbarManagement;

    public InventoryObjects inventoryObjects;

    public GameObject[] inventorySlots;

    public GameObject[] hotbarSlots;

    public bool canSelectTarget = false;

    public string objectSelected;

    public bool canStack;

    public bool canTalk = false;

    private int[] avalibeSlots;

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
                    objectSelected = hit.collider.tag;

                    ManageInventory(objectSelected, hit);
                }

                if (canTalk == true && hit.collider.CompareTag("NPC"))
                {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue();
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

    private void InventoryCheck(string objectSelected, RaycastHit hit)
    {
        HotbarCheck(objectSelected, hit);

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

                        UpdateStack(child, potionManagerHolder, hit);

                        break;
                    }
                    else
                    {
                        canStack = false;
                    }


                }
            }
            
        }

        
    }

    private void HotbarCheck(string objectSelected, RaycastHit hit)
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            Transform slot = hotbarSlots[i].transform;

            if (slot.childCount > 0 && slot.root.GetChild(0).CompareTag("HotbarSlot"))
            {
                GameObject child = slot.GetChild(0).gameObject;

                if (objectSelected == "potion" && child.CompareTag("potion"))
                {

                    PotionManager potionManagerHolder = child.GetComponent<PotionManager>();


                    string potionName = hit.collider.GetComponent<PotionHolder>().potion.potionName;

                    if (potionName == potionManagerHolder.potionName)
                    {
                        //Debug.Log("can stack");

                        canStack = true;

                        UpdateStack(child, potionManagerHolder, hit);

                        break;

                    }
                    else
                    {
                        canStack = false;
                    }


                }
            }
        }
    }

    private void UpdateStack(GameObject child, PotionManager potionHolder, RaycastHit hit)
    {
        TextMeshProUGUI textMeshPro = child.GetComponentInChildren<TextMeshProUGUI>();

        if (potionHolder.itemStack != 99)
        {
            potionHolder.itemStack++;
            textMeshPro.text = potionHolder.itemStack.ToString();
            Destroy(hit.collider.gameObject);
        }
        else
        {
            textMeshPro.text = potionHolder.itemStack.ToString();
        }
    }

    public void AddItemToSlot(string objectSelected, RaycastHit hit)
    {
        SpellBookHolder spellBookHolder = hit.collider.GetComponent<SpellBookHolder>();

        PotionHolder potionHolder = hit.collider.GetComponent<PotionHolder>();

        WeaponHolder weaponHolder = hit.collider.GetComponent<WeaponHolder>();

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
                }

                break;
            }
        }
    }

    // Method to add an item to a specific slot
    private void AddAttackToInventory(Transform slotTransform, string attack_ID)
    {
        for (int i = 0; i < inventoryObjects.attacks.Length; i++)
        {
            if (attack_ID == inventoryObjects.attacks[i].name)
            {
                Button newItem = Instantiate(inventoryObjects.attacks[i], slotTransform);

                newItem.transform.localPosition = Vector3.zero;

                //Debug.Log("Added item: " + newItem.name + " to slot: " + slotTransform.name);
            }
        }
        
    }

    private void AddPotionToInventory(Transform slotTransform, string potion_ID)
    {

        for (int i = 0; i < inventoryObjects.potions.Length; i++)
        {
            //Debug.Log(potion_ID + " | " + inventoryObjects.potions[i].name);

            if (potion_ID == inventoryObjects.potions[i].name)
            {
                Button newItem = Instantiate(inventoryObjects.potions[i], slotTransform);

                newItem.transform.localPosition = Vector3.zero;

                //Debug.Log("Added item: " + newItem.name + " to slot: " + slotTransform.name);
            }
        }
    }

    private void AddWeaponToInventory(Transform slotTransform, string weapon_ID)
    {
        //Debug.Log("I made it here");

        for (int i = 0; i < inventoryObjects.weapons.Length; i++)
        {
            if (weapon_ID == inventoryObjects.weapons[i].name)
            {
                Button newItem = Instantiate(inventoryObjects.weapons[i], slotTransform);

                newItem.transform.localPosition = Vector3.zero;

                //Debug.Log("Added item: " + newItem.name + " to slot: " + slotTransform.name);
            }
        }
    }
}
