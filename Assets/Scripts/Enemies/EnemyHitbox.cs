using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Enemy enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();
            if (player)
            {
                
                Vector2 direction = (player.transform.position - transform.position).normalized;
                Vector2 knockback = direction * enemy.GetKnockbackForce();
                player.TakeDamage((int)enemy.GetDamage(),knockback);
            }
        }
    }
}
