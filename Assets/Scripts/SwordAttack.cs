using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{


    [SerializeField] float damage = 5f;

    private Vector2 sideHitBoxOffset;
    
    public void Start(){
        
    }

    public void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Enemy"){
            print("Hit enemy!");
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null){
                enemy.TakeDamage(damage);
            }
        }
    }
}
