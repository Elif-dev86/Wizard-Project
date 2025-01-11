using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveStation : MonoBehaviour
{
    public InputActionAsset actions;

    private InputAction clickAction;

    public GameObject saveMenu;
    
    public bool canInteract;

    private void Awake() 
    {
        clickAction = actions.FindActionMap("Gameplay").FindAction("pointClick");

        clickAction.performed += OnClickPerformed;    
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

    public void Resume()
    {
        saveMenu.transform.gameObject.SetActive(false);
    }
}
