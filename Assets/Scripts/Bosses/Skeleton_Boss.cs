using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton_Boss : BossMachine
{
    private string[] attacks = { "attackTrigger_1", "attackTrigger_2", "attackTrigger_3"};

    [SerializeField] public float attackCoolDown = 3f;
    private float lastAttackTime;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private bool canSpecialAttack = true;

    [SerializeField]
    private bool jumpNow = false;

    [SerializeField]
    private bool aoeNow = false;

    [SerializeField]
    private bool canThrow = true;

    public GameObject projectilePrefab;

    public GameObject projectileSpawn;

    private float projectileProgress;
    

    protected void PerformAttack()
    {
        if (Time.time >= lastAttackTime + attackCoolDown)
        {
            lastAttackTime = Time.time;
            StartCoroutine(AttackTime());
        }
    }

    protected override void UpdateState()
    {
        float targetDistance = Vector3.Distance(transform.position, targets[0].transform.position);

        targetDistance = (int) targetDistance;

        switch (currentState)
        {

            case BossState.Idle:
                Debug.Log("I'm here");
                break;

            case BossState.Walking:

                if (targetDistance <= stopDistance)
                {
                    ChangeState(BossState.MeleeAttack);
                }
                else
                {
                    MoveTowardsTarget();
                }

                if (canSpecialAttack)
                {
                    TriggerRangeAttack();
                }


                if (jumpNow)
                {
                    CalculateDistanceBetweenObjects(rangePoint, this.gameObject);

                    ChangeState(BossState.RangedAttack);
                }

                if (aoeNow)
                {
                    ChangeState(BossState.SpecialAttack_1);
                }
                
                break;

            case BossState.MeleeAttack:

                Debug.Log("is attacking");
                if (targetDistance > stopDistance)
                {
                    ChangeState(BossState.Walking);
                }
                else
                {
                    PerformAttack();
                }

                break;

        }
    }

    protected override void OnStateEnter(BossState state)
    {
        switch (state)
        {

            case BossState.Idle:
                // Do something when entering idle state
                navAgent.isStopped = true;
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                //Debug.Log("Entering Idle state");
                break;

            case BossState.Walking:
                // For example, play a chasing animation or sound
                navAgent.isStopped = false;
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", true);
                //Debug.Log("Entering Chasing state");
                break;

            case BossState.MeleeAttack:
                // For example, play an attacking animation or sound
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
                navAgent.isStopped = true;
                //Debug.Log("Entering Attacking state");
                break;

            case BossState.RangedAttack:

                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                
                StartCoroutine(JumpToPoint());

                anim.SetBool("isThrowing", true);

                StartCoroutine(ThrowProjectile());

                break;

            case BossState.SpecialAttack_1:

                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);

                CalculateDistanceBetweenObjects(aoePoint, this.gameObject);

                StartCoroutine(PerformAOEAttack());

                break;
        }
    }

    private IEnumerator AttackTime()
    {
        navAgent.speed = 0;

        int randomAttack = RandomAttack(attacks.Length);

        Debug.Log(randomAttack);

        anim.SetTrigger(attacks[randomAttack]);

        yield return new WaitForSeconds(3f);

        navAgent.speed = moveSpeed;

    }

    private void LockToPlayer(Transform target)
    {
        // Calculate direction to look at the target
        Vector3 direction = target.transform.position - this.gameObject.transform.position;
        // Calculate the rotation required to look in that direction
        Quaternion lookRotation = Quaternion.LookRotation(direction * Time.deltaTime);
        // Assign the rotation to the game object
        this.gameObject.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, rotSpeed * Time.deltaTime);
    }

    private void CalculateDistanceBetweenObjects(GameObject objToCalculate, GameObject objOrigin)
    {
        navAgent.enabled = false;

        // Calculate direction to look at the target
        direction = (objToCalculate.transform.position - objOrigin.transform.position).normalized;

        startPoint = objOrigin.transform.position;

        endPoint = objToCalculate.transform.position;

        distance = Vector3.Distance(startPoint, endPoint);
    }

    private IEnumerator JumpToPoint()
    {

        this.gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);

        yield return new WaitForSeconds(1.2f);

        anim.SetTrigger("jumpStart");

         yield return new WaitForSeconds(.5f);
        
        while (progress < 1.1f)
        {

            // Update progress based on speed and distance
            progress += Time.deltaTime * speed / distance;

            // Move object in a straight line between start and target points
            Vector3 horizontalPosition = Vector3.Lerp(startPoint, endPoint, progress);

            // Calculate height for the parabola
            float parabolicHeight = height * Mathf.Sin(Mathf.PI * progress);

            // Apply the parabola to the vertical position
            Vector3 currentPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + parabolicHeight, horizontalPosition.z);

            // Move the object to the calculated position
            transform.position = currentPosition;

            yield return null;
        }

        anim.SetTrigger("jumpEnd");

        ResetJumpDistance();
        
    }

    private IEnumerator ThrowProjectile()
    {

        canThrow = false;

        jumpNow = false;

        yield return new WaitForSeconds(4);

        for (int i = 0; i < 15; i++)
        {

            anim.SetTrigger("throwProjectile");

            yield return new WaitForSeconds(.09f);

            GameObject projectile = Instantiate(projectilePrefab);

            CalculateDistanceBetweenObjects(targets[0].gameObject, projectileSpawn);

            while (projectileProgress < 1f)
            {

                projectile.transform.position = projectileSpawn.transform.position;

                // Update progress based on speed and distance
                projectileProgress += Time.deltaTime * projectileSpeed / distance;

                // Move object in a straight line between start and target points
                Vector3 horizontalPosition = Vector3.Lerp(startPoint, endPoint, projectileProgress);

                // Calculate height for the parabola
                float parabolicHeight = height * Mathf.Sin(Mathf.PI * projectileProgress);

                // Apply the parabola to the vertical position
                Vector3 currentPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + parabolicHeight, horizontalPosition.z);

                // Move the object to the calculated position
                projectile.transform.position = currentPosition;

                LockToPlayer(targets[0].transform);

                yield return null;
            }
            
            Destroy(projectile);

            projectileProgress = 0;

        }

        this.gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        
        anim.SetBool("isThrowing", false);
        anim.SetBool("isWalking", false);

        yield return new WaitForSeconds(3);

        ResetJumpDistance();

        canThrow = true;

        navAgent.enabled = true;

        canSpecialAttack = true;

        ChangeState(BossState.Walking);

    }

    private IEnumerator PerformAOEAttack()
    {
        aoeNow = false;

        StartCoroutine(JumpToPoint());

        yield return new WaitForSeconds(3f);
        
        anim.SetTrigger("shield_slam");

        yield return new WaitForSeconds(10);

        navAgent.enabled = true;

        canSpecialAttack = true;

        ChangeState(BossState.Walking);

    }

    private void TriggerRangeAttack()
    {
        float distanceFromAttackPoint = Vector3.Distance(rangePoint.transform.position, transform.position);

        int attackProbability = UnityEngine.Random.Range(1, 4000);
        
        if (distanceFromAttackPoint > 15f && attackProbability == 10)
        {
            jumpNow = true;
            canSpecialAttack = false;
        }
        else
        {
            TriggerAOEAttack();
        }
    }

    private void TriggerAOEAttack()
    {
        float distanceFromAttackPoint = Vector3.Distance(aoePoint.transform.position, transform.position);

        int attackProbability = UnityEngine.Random.Range(1, 4000);
        
        if (distanceFromAttackPoint > 15f && attackProbability == 10)
        {
            aoeNow = true;
            canSpecialAttack = false;
        }
    }

    private void ResetJumpDistance()
    {
        startPoint = Vector3.zero;

        endPoint = Vector3.zero;

        distance = 0;

        progress = 0;
    } 

    protected int RandomAttack(int attackIndex)
    {
        return UnityEngine.Random.Range(0, attackIndex);
    }
}
