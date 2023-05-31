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
            //TryMove(movementDirection);
            rigidbody2d.AddForce(movementDirection * speed * 1000 * Time.deltaTime);

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
        ReduceCooldown();
        DecideAction();
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
