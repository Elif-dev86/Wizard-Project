using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] posibleItems;

    public GameObject[] itemSlots;

    //[HideInInspector]
    public bool isOpen;

    public bool isRandomItems, isSelectedItem;
    
    public int chestID;

    private bool[] chestType;

    Animator anim;

    BoxCollider chestCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        chestCollider = GetComponent<BoxCollider>();

        chestType = new bool[] {isRandomItems, isSelectedItem};

    }

    public void OpenChest()
    {
        if (!isOpen)
        {
            if (chestType[0])
            {
                anim.SetTrigger("openChest");
                chestCollider.enabled = false;

                for (int i = 0; i < itemSlots.Length; i++)
                {

                    int itemsToServe = SetRandomItems();
                    
                    GameObject itemToGenerate = Instantiate(posibleItems[itemsToServe]);

                    itemToGenerate.transform.position = itemSlots[i].transform.position;

                    itemToGenerate.transform.SetParent(itemSlots[i].transform);
                    
                }
            }
            else if (chestType[1])
            {
                for (int i = 0; i < itemSlots.Length; i++)
                {

                    anim.SetTrigger("openChest");
                    chestCollider.enabled = false;
                    
                    GameObject itemToGenerate = Instantiate(posibleItems[i]);

                    itemToGenerate.transform.position = itemSlots[i].transform.position;

                    itemToGenerate.transform.SetParent(itemSlots[i].transform);
                    
                }
            }

        }
        else
        {
            return;
        }

    }

    public void ChestIsOpened()
    {
        if (isOpen)
        {
            anim.SetTrigger("openChest");
            chestCollider.enabled = false;
        }
    }

    int SetRandomItems()
    {
        int randomItem = Random.Range(0, posibleItems.Length);

        return randomItem;
    }
}
