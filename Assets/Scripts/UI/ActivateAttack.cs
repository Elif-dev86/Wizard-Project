using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAttack : MonoBehaviour
{

    public void CanSelectTarget()
    {
        HotbarManagement hotbar = FindObjectOfType<HotbarManagement>();

        InventoryManagement inventory = FindObjectOfType<InventoryManagement>();

        AttackEvent aEvent = FindObjectOfType<AttackEvent>();

        aEvent.attackSelected = this.gameObject.name;

        hotbar.canSelectTarget = true;
        inventory.canSelectTarget = true;

    }
}
