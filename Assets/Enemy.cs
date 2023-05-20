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
    private float knockbackForce;
    [SerializeField]
    private float trustGain = 10f;

    protected float health;

    private bool isAlive = true;
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    private Transform healthBar;
    private EnemyAIAstar ai;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAIAstar>();
        healthBar = transform.Find("EnemyHealthbar").GetComponent<Transform>();
        UpdateHealthbar(1);
    }

    private void UpdateHealthbar(float xScale)
    {
        healthBar.localScale = new Vector3(xScale, healthBar.localScale.y, healthBar.localScale.z);
    }

    public float GetKnockbackForce()
    {
        return knockbackForce;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        health = health < 0 ? 0 : health;
        UpdateHealthbar(health/maxHealth);
        if (health <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage, Vector2 knockback)
    {
        health -= damage;
        health = health < 0 ? 0 : health;
        UpdateHealthbar(health / maxHealth);
        ai.SearchForPlayer();
        rigidbody2d.AddForce(knockback, ForceMode2D.Impulse);
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        GameManager.Instance.player.GetComponent<Player>().ChangeTrust(trustGain);
        animator.SetTrigger("Dead");
        ai.SetState(EnemyState.Dead);
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
