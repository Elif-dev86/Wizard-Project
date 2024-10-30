using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotbarManagement : MonoBehaviour
{
    InventoryManagement inventoryManagement;

    public InputActionAsset actions;

    private InputAction clickAction;

    public GameObject attackTarget;

    public GameObject testRecticle;

    public int itemStack = 0;

    public string attackSelected;

    public bool canSelectTarget = false;

    [SerializeField]
    private PlayerMovement pMovement;

    [SerializeField]
    public GameObject[] hotBarSlots;

    [SerializeField]
    Button[] choices;

    private void Awake()
    {
        clickAction = actions.FindActionMap("Gameplay").FindAction("pointClick");

        clickAction.performed += OnClickPerformed;

        choices = new Button[hotBarSlots.Length];
    }


    void Start()
    {
        pMovement = FindObjectOfType<PlayerMovement>();

        inventoryManagement = FindObjectOfType<InventoryManagement>();
    }

    private void FixedUpdate()
    {
        FindButtons();

        if (canSelectTarget != false)
        {
            testRecticle.SetActive(true);

            AttackPointer();
            
        }
        else
        {
            testRecticle.SetActive(false);
        }
        
    }

    private void FindButtons()
    {

        for (int i  = 0; i < hotBarSlots.Length; i++)
        {
            Transform slot = hotBarSlots[i].transform;

            if (slot.childCount > 0 )
            {
                GameObject child = slot.GetChild(0).gameObject;

                choices[i] = child.GetComponent<Button>();
            }
        }

        /*foreach (Button btn in choices)
        {
            if (btn != null)
            {
                Button choice = btn;
                btn.onClick.AddListener(() => AttackSelect(choice));
            }
            
        }*/

    }

    /*public void AttackSelect(Button choice)
    {
        attackSelected = choice.gameObject.tag;

        Debug.Log(attackSelected);

        if (attackSelected == "potion" || attackSelected == "weapon")
        {
            return;
        }
        else
        {
            canSelectTarget = true;
            inventoryManagement.canSelectTarget = true;
            return;
        }
        
        
    }*/

    private void AttackPointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            testRecticle.transform.position = hit.point;
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (canSelectTarget == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (context.performed)
                {
                    GameObject gameObj = Instantiate(attackTarget);
                    gameObj.transform.position = hit.point;

                    TargetManager.AddTarget(gameObj);

                    pMovement.RotateToTarget(gameObj);

                    Destroy(gameObj, .5f);
                }
            }
            canSelectTarget = false;
            inventoryManagement.canSelectTarget = false;
        }
    }

    public void OnEnable()
    {
        clickAction.Enable();
    }

    public void OnDisable()
    {
        clickAction.Disable();
    }
}
