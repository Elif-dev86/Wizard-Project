using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossMachine : MonoBehaviour
{
    public enum BossState
    {
        BattleStart,
        Idle,
        Walking,
        MeleeAttack,
        RangedAttack,
        SpecialAttack_1,
        SpecialAttack_2,
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

    [SerializeField]
    protected GameObject rangePoint;

    [SerializeField]
    protected GameObject aoePoint;

    protected Vector3 direction;

    [SerializeField]
    protected float speed = 10f;

    protected float progress = 0f;

    protected float distance;

    [SerializeField]
    protected float height = 5f;

    protected Vector3 startPoint;

    protected Vector3 endPoint;

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
    }

    protected virtual void Update()
    {
        UpdateState();
    }

    protected virtual void UpdateState()
    {

        switch (currentState)
        {

            case BossState.BattleStart:

                break;

            case BossState.Idle:
                
                break;

            case BossState.Walking:
                
                break;

            case BossState.MeleeAttack:

                break;

            case BossState.RangedAttack:

            break;

            case BossState.SpecialAttack_1:

                break;

            case BossState.SpecialAttack_2:

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

        ChangeState(BossState.Walking);
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

            case BossState.Walking:
                // For example, play a chasing animation or sound
                break;

            case BossState.MeleeAttack:
                // For example, play an attacking animation or sound
                break;
        }
    }
}
