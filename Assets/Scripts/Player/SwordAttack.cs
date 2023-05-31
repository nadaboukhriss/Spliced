using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    private PlayerController player;
    public void Start(){
        player = GetComponentInParent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
             Enemy enemy = other.transform.parent.GetComponent<Enemy>();
             if(enemy != null){
                Vector2 knockbackDirection = ((Vector2)(enemy.transform.position - player.transform.position)).normalized;
                Vector2 knockback = knockbackDirection * player.GetCurrentShape().GetKnockbackForce();
                enemy.TakeDamage(player.GetCurrentShape().GetBasicDamage(), knockback);
             }
        }
    }
}
