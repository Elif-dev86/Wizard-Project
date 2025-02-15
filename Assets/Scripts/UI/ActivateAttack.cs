using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ActivateAttack : MonoBehaviour
{

    [SerializeField]
    private bool inInvertory;

    public bool isSelected = false;

    private Button button;

    public void CanSelectTarget()
    {
        HotbarManagement hotbar = FindObjectOfType<HotbarManagement>();

        AttackEvent aEvent = FindObjectOfType<AttackEvent>();
        
        if (!this.isSelected)
        {
            isSelected = true;

            aEvent.attackSelected = this.gameObject.name;

            hotbar.canSelectTarget = true;
        }
        else
        {
            isSelected = false;

            aEvent.attackSelected = null;

            hotbar.canSelectTarget = false;
            return;
        }

    }

    void FixedUpdate()
    {

        if (CheckForButtonPosition(inInvertory))
        {
            button = GetComponent<Button>();

            button.enabled = false;
        }
        else
        {
            button = GetComponent<Button>();

            button.enabled = true;
        }
    }

    bool CheckForButtonPosition(bool isInPosition)
    {
        Transform slotType = GetComponent<Transform>();

        if (slotType.parent.CompareTag("inventorySlot"))
        {
            inInvertory = true;
        }
        else
        {
            inInvertory= false;
        }

        return inInvertory;
    }

}
