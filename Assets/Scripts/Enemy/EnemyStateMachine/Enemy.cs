using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IEnemyDamageable
{
    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }

    protected NavMeshAgent navAgent;

    [SerializeField] private Slider enemyHealthCanvas;

    [SerializeField] protected string enemyName;
    [SerializeField] protected int enemyMaxHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotSpeed;
    [SerializeField] protected float followDistance;
    [SerializeField] protected float stopDistance;
    [SerializeField] protected float gravity = -12;

    protected float velocityY;
    protected Transform target;
    protected Animator anim;

    protected EnemyState currentState;

    protected virtual void Awake()
    {
        enemyHealthCanvas.maxValue = 1;
        enemyHealthCanvas.value = 1;
    }

    protected virtual void Start()
    {

        enemyHealthCanvas.maxValue = enemyMaxHealth;
        enemyHealthCanvas.value = enemyMaxHealth;

        navAgent = GetComponent<NavMeshAgent>();
        if (!navAgent)
        {
            navAgent = GetComponentInChildren<NavMeshAgent>();
        }

        anim = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        Introduction();
        ChangeState(EnemyState.Idle);
    }

    protected virtual void FixedUpdate()
    {
        UpdateState();
    }

    protected virtual void Introduction()
    {
        //Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint + ", moveSpeed: " + moveSpeed);
    }

    protected virtual void UpdateState()
    {
        float objDistance = Vector3.Distance(transform.position, target.position);

        objDistance = (int) objDistance;

        switch (currentState)
        {
            case EnemyState.Idle:
                if (objDistance <= followDistance)
                {
                    ChangeState(EnemyState.Chasing);
                }
                break;

            case EnemyState.Chasing:
                if (objDistance > followDistance)
                {
                    
                    ChangeState(EnemyState.Idle);
                }
                else if (objDistance <= stopDistance)
                {
                    ChangeState(EnemyState.Attacking);
                }
                else
                {
                    MoveTowardsTarget();
                }
                break;

            case EnemyState.Attacking:
                if (objDistance > stopDistance)
                {
                    ChangeState(EnemyState.Chasing);
                }
                else
                {
                    PerformAttack();
                }
                break;
        }
    }

    protected void ChangeState(EnemyState newState)
    {
        currentState = newState;
        OnStateEnter(newState);
    }

    protected virtual void OnStateEnter(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                // Do something when entering idle state
                break;

            case EnemyState.Chasing:
                // For example, play a chasing animation or sound
                break;

            case EnemyState.Attacking:
                // For example, play an attacking animation or sound
                break;
        }
    }

    protected void MoveTowardsTarget()
    {
        // Calculate the direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Ensure the y-component of the direction is zero to keep the enemy on the ground
        direction.y = 0;

        // Move the enemy towards the target
        navAgent.SetDestination(target.position);

        //RotateToTarget(target);
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        Debug.Log($"Triggered with: {other.name}");
        SpellHolder spellHolder = other.gameObject.GetComponent<SpellHolder>();
        Debug.Log($"SpellHolder component: {spellHolder}");

        if (spellHolder != null)
        {
            TakeDamage(spellHolder.spell);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }

    public void TakeDamage(Spell spell)
    {
        enemyHealthCanvas.value -= spell.spellDamage;
        Debug.Log("Enemy damaged by " + spell.spellName + " for " + spell.spellDamage + " damage.");

        if (enemyMaxHealth <= 0)
        {
            Debug.Log("Enemy died.");
        }
    }

    // Method to check if the enemy is grounded
    protected virtual bool IsGrounded()
    {
        // Raycast down to check if the enemy is on the ground
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);
    }

    protected virtual void RotateToTarget(Transform target)
    {
        // Calculate direction to look at the target
        Vector3 direction = target.transform.position - this.gameObject.transform.position;
        // Calculate the rotation required to look in that direction
        Quaternion lookRotation = Quaternion.LookRotation(direction * Time.deltaTime);
        // Assign the rotation to the game object
        this.gameObject.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, rotSpeed * Time.deltaTime);
    }

    // Method to perform the attack
    protected abstract void PerformAttack();
}
