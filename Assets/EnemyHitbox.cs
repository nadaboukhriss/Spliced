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
                player.TakeDamage((int)enemy.GetDamage());
            }
        }
    }
}
