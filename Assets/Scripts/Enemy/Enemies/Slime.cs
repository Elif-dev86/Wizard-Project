using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
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
                //Debug.Log("Entering Idle state");
                break;

            case EnemyState.Chasing:
                // For example, play a chasing animation or sound
                navAgent.isStopped = false;
                break;

            case EnemyState.Attacking:
                // For example, play an attacking animation or sound
                navAgent.isStopped = true;
                break;
        }
    }

    private IEnumerator AttackTime()
    {
        navAgent.speed = 0;

        yield return new WaitForSeconds(3f);

        navAgent.speed = moveSpeed;

    }
}
