using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy1AI : EnemyAIAstar
{

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float projectileSpeed = 10f;

    public void Update()
    {
        ReduceCooldown();
        DecideAction();
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
            //TryMove(movementDirection);
            rigidbody2d.AddForce(movementDirection * speed * 1000 * Time.deltaTime);

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
        projectileInstance.GetComponent<Projectile>().SetParameters(enemy.GetDamage(), enemy.GetKnockbackForce(), direction * projectileSpeed);

        // SFX
        SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.NoFaceAttackSound.clip);
    }
}
