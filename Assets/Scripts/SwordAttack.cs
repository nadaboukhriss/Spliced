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
        // if(other.tag == "Enemy"){
        //     Enemy enemy = other.transform.parent.GetComponent<Enemy>();
        //     if(enemy != null){
        //         print("hit");
        //         enemy.TakeDamage(damage);
        //     }
        // }
    }
}
