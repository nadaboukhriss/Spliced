using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject rangePrefab;

    protected float health;

    private bool isAlive = true;
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    private EnemyAIAstar ai;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAIAstar>();
    }



    public void TakeDamage(float damage)
    {
        health -= damage;
        health = health < 0 ? 0 : health;
        //healthbar.transform.localScale = new Vector3(health/maxHealth,healthbar.transform.localScale.y,healthbar.transform.localScale.z);
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            ai.SetState(EnemyState.Dead);
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
    public void Defeated()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    protected virtual void Attack()
    {
        
    }
}
