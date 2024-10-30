using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{

    private string[] attacks = { "attackTrigger_1", "attackTrigger_2"};
    [SerializeField] private float attackCoolDown = 2f;
    private float lastAttackTime;

    protected override void PerformAttack()
    {
        if (Time.time >= lastAttackTime + attackCoolDown)
        {
            lastAttackTime = Time.time;
            StartCoroutine(AttackTime());
        }
    }

    protected override void OnStateEnter(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                // Do something when entering idle state
                navAgent.isStopped = true;
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                //Debug.Log("Entering Idle state");
                break;

            case EnemyState.Chasing:
                // For example, play a chasing animation or sound
                navAgent.isStopped = false;
                anim.SetBool("isWalking", true);
                Debug.Log("Entering Chasing state");
                break;

            case EnemyState.Attacking:
                // For example, play an attacking animation or sound
                anim.SetBool("isWalking", false);
                navAgent.isStopped = true;
                Debug.Log("Entering Attacking state");
                break;
        }
    }

    private IEnumerator AttackTime()
    {
        navAgent.speed = 0;

        int randomAttack = RandomAttack(attacks.Length);

        anim.SetTrigger(attacks[randomAttack]);

        yield return new WaitForSeconds(1.21f);

        navAgent.speed = moveSpeed;

    }

    private int RandomAttack(int attackIndex)
    {
        return UnityEngine.Random.Range(0, attackIndex);
    }
}
