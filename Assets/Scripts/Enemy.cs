using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 10f;
    Animator animator;

    public void Start(){
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            animator.SetTrigger("Dead");
        }
    }

    public void Defeated(){
        
        Destroy(gameObject);
    }

}
