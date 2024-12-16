using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] posibleItems;

    public GameObject[] itemSlots;

    public bool isOpen;

    Animator anim;

    BoxCollider chestCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        chestCollider = GetComponent<BoxCollider>();
    }

    public void OpenChest()
    {
        if (!isOpen)
        {

            anim.SetTrigger("openChest");

            for (int i = 0; i < itemSlots.Length; i++)
            {

                int itemsToServe = SetRandomItems();
                
                GameObject itemToGenerate = Instantiate(posibleItems[itemsToServe]);

                itemToGenerate.transform.position = itemSlots[i].transform.position;

                itemToGenerate.transform.SetParent(itemSlots[i].transform);
                
            }
        }
        else
        {
            return;
        }

    }

    int SetRandomItems()
    {
        int randomItem = Random.Range(0, posibleItems.Length);

        return randomItem;
    }
}
