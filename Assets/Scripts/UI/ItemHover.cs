using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject itemDescription;

    private ActivateAttack activateAttack;

    public String itemName;

    public String itemDescriptionText;

    Vector3 rectPos;

    private void Start() 
    {
        itemDescription = GameObject.FindGameObjectWithTag("itemDescription");

        activateAttack = GetComponent<ActivateAttack>();

        rectPos = itemDescription.transform.position;

        rectPos.x = 2139;

        itemDescription.transform.position = rectPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (activateAttack.inInvertory)
        {
            rectPos.x = 1139;

            itemDescription.transform.position = rectPos;

            itemDescription.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;
            itemDescription.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemDescriptionText;

        }
        else
        {
            rectPos.x = 2139;

            itemDescription.transform.position = rectPos;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (activateAttack.inInvertory)
        {
            rectPos.x = 2139;

            itemDescription.transform.position = rectPos;
        }
        else
        {
            rectPos.x = 2139;

            itemDescription.transform.position = rectPos;
        }
    }
}
