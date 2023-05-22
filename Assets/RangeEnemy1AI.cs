using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy1AI : EnemyAIAstar
{

    [SerializeField]
    private GameObject projectilePrefab;

    public void Update()
    {
        if (state == EnemyState.Dead)
        {
            rigidbody2d.velocity = Vector2.zero;
            return;
        }
        //Cooldown for the enemy attacking
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
        }
        else
        {
            state = EnemyState.Walking;
            returnToStart = true;
            //state = EnemyState.Idle;
        }
    }
    void FixedUpdate()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            //rigidbody2d.velocity = Vector2.zero;
            state = EnemyState.Idle;
            reachedEndOfPath = true;
            return;
        }

        if (state == EnemyState.Walking && canMove)
        {

            movementDirection = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;
            TryMove(movementDirection);

            animator.SetFloat("XInput", movementDirection.x);
            animator.SetFloat("YInput", movementDirection.y);

            float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }


    }
    override
    protected void Attack()
    {
        currentCooldown = attackCooldown;
        GameObject projectileInstance = Instantiate(projectilePrefab);

        Vector2 direction = ((Vector2)(target.position - transform.position)).normalized;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        projectileInstance.transform.position = transform.position;
        projectileInstance.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        projectileInstance.GetComponent<Projectile>().travelDirection = direction;
        projectileInstance.GetComponent<Projectile>().SetDamage(enemy.GetDamage());

        // SFX
        SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.NoFaceAttackSound);
    }

    /*override
    public void TakeDamage(float damage)
    {
        // SFX
        SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.NoFaceHitSound);
        TakeDamage(damage);
    }

    override
    public void Defeated()
    {
        // SFX
        SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.NoFaceDeathSound);
        Defeated();
    }
    */
}
