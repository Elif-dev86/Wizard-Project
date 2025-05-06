using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{

    // create two states (open and closed)
    public enum DoorState
    {
        Open,
        Closed
    }

    public InputActionAsset actions;

    protected InputAction clickAction;

    // Inventory management
    protected InventoryManagement iManagement;

    public bool isOpen;
    
    protected virtual void Start()
    {
        clickAction = actions.FindActionMap("Gameplay").FindAction("pointClick");

        clickAction.performed += CheckForKey;

        iManagement = GameObject.FindObjectOfType<InventoryManagement>();
    }

    protected virtual void CheckForKey(InputAction.CallbackContext context)
    {

        if (iManagement.canSelectTarget != true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (context.performed)
                {
                    string doorSelected;

                    doorSelected = hit.collider.tag;

                    iManagement.InventoryCheck(doorSelected, hit);
                }
            }
        }
        
    }
}
