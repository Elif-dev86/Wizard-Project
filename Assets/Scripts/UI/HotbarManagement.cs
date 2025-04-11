using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotbarManagement : MonoBehaviour
{
    InventoryManagement inventoryManagement;

    ActivateAttack attackIsSelected;

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

        attackIsSelected = FindObjectOfType<ActivateAttack>();
        
    }

    private void Update()
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

        if (!pMovement)
        {
            pMovement = FindObjectOfType<PlayerMovement>();
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

    }

    private void AttackPointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            testRecticle.transform.position = hit.point;
        }
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
                    CheckForInitiatedAttack();

                    GameObject gameObj = Instantiate(attackTarget);
                    gameObj.transform.position = hit.point;

                    TargetManager.AddTarget(gameObj);

                    pMovement.RotateToTarget(gameObj);

                    StartCoroutine(pMovement.AttackTime());
                    StopCoroutine(pMovement.AttackTime());

                    Destroy(gameObj, .5f);
                }
            }
            canSelectTarget = false;
            inventoryManagement.canSelectTarget = false;

            for (int i = 0; i < hotBarSlots.Length; i++)
            {
                Transform slot = hotBarSlots[i].transform;

                if (slot.childCount > 0 )
                {
                    GameObject child = slot.GetChild(0).gameObject;
                    
                    child.GetComponent<ActivateAttack>().isSelected = false;
                }
            }

        }
    }

    void CheckForInitiatedAttack()
    {
        for(int i = 0; i < hotBarSlots.Length; i++)
        {
            Transform slot = hotBarSlots[i].transform;

            if (slot.childCount > 0 )
            {
                GameObject child = slot.GetChild(0).gameObject;

                ActivateAttack activateAttack = child.GetComponent<ActivateAttack>();

                if (activateAttack.isSelected)
                {
                    CoolDownTimer coolDown = child.GetComponentInChildren<CoolDownTimer>();

                    coolDown.image.enabled = true;
                    coolDown.timerValue = 1;

                    coolDown.startTimer = true;
                }
            }
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
