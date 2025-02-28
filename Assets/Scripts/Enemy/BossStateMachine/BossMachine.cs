using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMachine : MonoBehaviour
{
    public enum BossState
    {
        BattleStart,
        Idle,
        Melee,
        Attacking
    }

    protected NavMeshAgent navAgent;

    protected CharacterController bossController;

    protected Animator anim;

    protected BossState currentState;

    [SerializeField] protected List<Transform> targets = new List<Transform>();

    [SerializeField] protected string enemyName;
    [SerializeField] protected int enemyMaxHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotSpeed;
    [SerializeField] protected float stopDistance;
    protected float velocityY;

    Transform target;

    protected virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        bossController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        navAgent = GetComponent<NavMeshAgent>();
        if (!navAgent)
        {
            navAgent = GetComponentInChildren<NavMeshAgent>();
        }

        navAgent.speed = moveSpeed;
        navAgent.stoppingDistance = stopDistance;

        StartCoroutine(BattleInitializer());
        StopCoroutine(BattleInitializer());
    }

    protected virtual void Update()
    {
        UpdateState();
    }

    protected virtual void UpdateState()
    {

        float targetDistance = Vector3.Distance(transform.position, targets[0].transform.position);

        targetDistance = (int) targetDistance;

        switch (currentState)
        {

            case BossState.BattleStart:

                //StartCoroutine(BattleInitializer());
                //StopCoroutine(BattleInitializer());

                break;

            case BossState.Idle:
                
                break;

            case BossState.Melee:

                if (targetDistance <= stopDistance)
                {
                    ChangeState(BossState.Attacking);
                }
                else
                {
                    MoveTowardsTarget();
                }
                
                break;

            case BossState.Attacking:

                Debug.Log("is attacking");
                if (targetDistance > stopDistance)
                {
                    ChangeState(BossState.Melee);
                }

                break;
        }
    }

    protected void ChangeState(BossState newState)
    {
        currentState = newState;
        OnStateEnter(newState);
    }

    protected void MoveTowardsTarget()
    {
        //Calculate the direction towards the target
        Vector3 direction = targets[0].transform.position - transform.position;

        // Ensure the y-component of the direction is zero to keep the enemy on the ground
        direction.y = 0;

        // Move the enemy towards the target
        navAgent.SetDestination(targets[0].transform.position);
    }

    
    protected IEnumerator BattleInitializer()
    {
        Debug.Log("Starting battle");

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        targets.Add(target);

        navAgent.isStopped = true;

        anim.SetBool("isIdle", true);

        yield return new WaitForSeconds(3);

        navAgent.isStopped = false;

        ChangeState(BossState.Melee);
    }
    
    protected virtual void OnStateEnter(BossState state)
    {
        switch (state)
        {
            case BossState.BattleStart:
                // Do something when entering idle state
                break;

            case BossState.Idle:
                // Do something when entering idle state
                break;

            case BossState.Melee:
                // For example, play a chasing animation or sound
                break;

            case BossState.Attacking:
                // For example, play an attacking animation or sound
                break;
        }
    }
}
