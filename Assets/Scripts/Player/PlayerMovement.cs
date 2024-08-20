
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour, IPlayerDamageable
{
    PhotonView view;

    CharacterController controller;

    DialogueManager dialogueManager;

    public GameObject inventory;

    public Slider healtBar;

    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public InputActionAsset actions;

    [HideInInspector]
    public InputAction moveAction;

    Transform inventoryTransform;

    Vector3 newPosition;

    public bool canMove;

    public bool isToggle = true;

    public int maxHealth;

    public float gravity = -12;

    private float velocityY;

    private Vector3 moveDirection;

    private float targetAngle;

    [SerializeField]
    float moveSpeed = 5f;
   
    float lastDirectionX = 0;
    float lastDirectionZ = 0;

    private void Awake()
    {
        moveAction = actions.FindActionMap("gameplay").FindAction("movement");

        actions.FindActionMap("gameplay").FindAction("toggleInventory").performed += ToggleInventory;

        canMove = true;
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        healtBar = (Slider)FindObjectOfType(typeof(Slider));

        inventory = GameObject.FindGameObjectWithTag("InventorySlot");

        inventoryTransform = inventory.transform;

        healtBar.maxValue = maxHealth;
        healtBar.value = maxHealth;

        if (isToggle == true)
        {
            newPosition = inventoryTransform.position;

            newPosition.x = 2150;

            inventoryTransform.position = newPosition;
        }

        canMove = true;

    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (canMove == true)
            {
                MovementHandler();
            }
        }
    }

    void MovementHandler()
    {
        // Read the movement input as a Vector3
        Vector3 moveVector = moveAction.ReadValue<Vector3>();

        // Create a new vector for movement direction, ignoring the y-axis
        moveDirection = new Vector3(moveVector.x, 0, moveVector.z);

        // Check if the movement vector's magnitude is greater than or equal to 0.1
        if (moveVector.magnitude >= 0.1f)
        {
            // Store the x and z components of the move vector
            lastDirectionX = moveVector.x;
            lastDirectionZ = moveVector.z;

            // Calculate the target angle for rotation based on the movement direction
            targetAngle = Mathf.Atan2(lastDirectionX, lastDirectionZ) * Mathf.Rad2Deg;

            // Rotate the character towards the target angle
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Apply gravity to the y-velocity
            velocityY += Time.deltaTime * gravity;

            // Calculate the velocity vector combining movement direction, speed, and gravity
            Vector3 velocity = transform.forward + moveDirection * moveSpeed + Vector3.up * velocityY;

            // Move the character controller by the calculated velocity
            controller.Move(velocity * Time.deltaTime);

            // Reset y-velocity if the character is grounded
            if (controller.isGrounded)
            {
                velocityY = 0f;
            }
            else
            {
                velocityY = -12f;
            }
        }
    }

    public void RotateToTarget(GameObject target)
    {
        // Calculate direction to look at the target
        Vector3 direction = target.transform.position - this.gameObject.transform.position;

        direction.y = 0;

        // Calculate the rotation required to look in that direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Assign the rotation to the game object
        this.gameObject.transform.rotation = lookRotation;

        StartCoroutine(AttackTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAttackHolder enemyAttackHolder = other.gameObject.GetComponent<EnemyAttackHolder>();

        if (enemyAttackHolder != null)
        {
            TakeEnemyDamage(enemyAttackHolder.enemyAttack);
        }

        if (other.CompareTag("talkDistance"))
        {
            inventory.GetComponent<InventoryManagement>().canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inventory.GetComponent<InventoryManagement>().canTalk = false;

        dialogueManager.anim.SetBool("isOpen", false);
    }

    public void TakeEnemyDamage(EnemyAttack enemyAttack)
    {
        healtBar.value -= enemyAttack.enemyDamage;
        //Debug.Log("Player damaged by " + enemyAttack.enemyName + " for " + enemyAttack.enemyDamage + " damage.");
    }

    IEnumerator AttackTime()
    {
        canMove = false;

        anim.SetTrigger("attackTrigger");

        yield return new WaitForSeconds(1f);

        canMove = true;
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {

        if (context.performed)
        {

            if (isToggle)
            {

                newPosition = inventoryTransform.position;

                newPosition.x = 1500;

                inventoryTransform.position = newPosition;

            }
            else
            {


                newPosition = inventoryTransform.position;

                newPosition.x = 2150;

                inventoryTransform.position = newPosition;


            }

            isToggle = !isToggle;
        }

    }

    public void OnEnable()
    {
        moveAction.Enable();
        actions.Enable();
    }

    public void OnDisable()
    {
        moveAction.Disable();
        actions.Disable();
    }
}
