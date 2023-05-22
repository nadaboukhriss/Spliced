using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : EnemyAIAstar
{
    AudioSource audioSourceStepSound;

    void FixedUpdate()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            //rigidbody2d.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            state = EnemyState.Idle;
            reachedEndOfPath = true;
            return;
        }

        if (state == EnemyState.Walking && canMove)
        {

            movementDirection = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;
            TryMove(movementDirection);

            animator.SetBool("isWalking", true);

            animator.SetFloat("XInput", movementDirection.x);
            animator.SetFloat("YInput", movementDirection.y);

            float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }


    }

    public void Update()
    {
        if (state == EnemyState.Dead)
        {
            rigidbody2d.velocity = Vector2.zero;
            return;
        }
        ReduceCooldown();

        bool canSeeTarget = CanSeeTarget();
        // Check if we are in range of attacking the player
        if (InRangeOfAttack())
        {
            state = EnemyState.Attacking;
            path = null; //Remove the path we are walking
            if (IsAbilityReady())
            {
                Attack();
            }
        }
        else if (canSeeTarget || Time.time - lastSeenTarget < rememberTargetTime)
        {
            state = EnemyState.Walking;
            // SFX on
            /*audioSourceStepSound = gameObject.AddComponent<AudioSource>();
            audioSourceStepSound.clip = SFXManager.sfxinstance.SkellyStepSound;
            audioSourceStepSound.loop = true;
            audioSourceStepSound.Play();*/
        }
        else
        {
            state = EnemyState.Walking;
            returnToStart = true;
            //state = EnemyState.Idle;
            // SFX off
            //SFXManager.sfxinstance.Audio.Stop(SFXManager.sfxinstance.SkellyStepSound);
        }
    }


    override
    protected void Attack()
    {
        currentCooldown = attackCooldown;
        Vector2 direction = ((Vector2)(target.position - transform.position)).normalized;

        animator.SetFloat("XInput", direction.x);
        animator.SetFloat("YInput", direction.y);
        
        animator.SetTrigger("Attack");
    }
}
