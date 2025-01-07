using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ActivateAttack : MonoBehaviour
{

    public bool isSelected = false;

    public void CanSelectTarget()
    {
        HotbarManagement hotbar = FindObjectOfType<HotbarManagement>();

        InventoryManagement inventory = FindObjectOfType<InventoryManagement>();

        AttackEvent aEvent = FindObjectOfType<AttackEvent>();
        
        if (!this.isSelected)
        {
            isSelected = true;

            aEvent.attackSelected = this.gameObject.name;

            hotbar.canSelectTarget = true;
            inventory.canSelectTarget = true;
        }
        else
        {
            isSelected = false;

            hotbar.canSelectTarget = false;
            inventory.canSelectTarget = false;
            return;
        }

    }

}
