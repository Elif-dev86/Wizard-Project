using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class BossMachine : MonoBehaviour
{
    public enum BossState
    {
        StandBy,
        BattleStart,
        Idle,
        Walking,
        MeleeAttack,
        RangedAttack,
        SpecialAttack_1,
        SpecialAttack_2,
        DeadState
    }

    protected NavMeshAgent navAgent;

    protected CharacterController bossController;

    protected Animator anim;

    protected BossState currentState;

    [SerializeField] protected List<Transform> targets = new List<Transform>();

    [SerializeField] public Slider bossHealthSlider;

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

    [SerializeField]
    Animator camAnim;

    protected virtual void Start()
    {
        bossHealthSlider = GameObject.FindGameObjectWithTag("bossHealth").GetComponent<Slider>();

        bossHealthSlider.gameObject.SetActive(false);

        bossHealthSlider.maxValue = enemyMaxHealth;
        bossHealthSlider.value = enemyMaxHealth;

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

        ChangeState(BossState.StandBy);
    }

    protected virtual void Update()
    {
        UpdateState();
    }

    protected virtual void UpdateState()
    {

        switch (currentState)
        {

            case BossState.StandBy:
            
                break;

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

            case BossState.DeadState:

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

    
    protected virtual IEnumerator BattleInitializer()
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

    public void TakeDamage(Spell spell)
    {
        bossHealthSlider.value -= spell.spellDamage;
        Debug.Log("Enemy damaged by " + spell.spellName + " for " + spell.spellDamage + " damage.");

        if (bossHealthSlider.value <= 0)
        {
            StopAllCoroutines();
            ChangeState(BossState.DeadState);
            StartCoroutine(DeathTrigger());
        }
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
    
    private IEnumerator DeathTrigger()
    {
        bossHealthSlider.gameObject.SetActive(false);

        this.gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);

        anim.SetBool("isThrowing", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
        anim.SetTrigger("deathTrigger");

        navAgent.enabled = false;
        
        yield return new WaitForSeconds(7f);
        
        camAnim.SetBool("zoomOut", false);
        
        yield return new WaitForSeconds(.8f);

        Destroy(this.gameObject);
    }
}
