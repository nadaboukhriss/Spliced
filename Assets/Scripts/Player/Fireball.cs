using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public float dirRight;
    public float dirUp;
    public float maxTimeAlive = 3f;

    private float timeAlive = 0f;
    private bool isDead;

    public Vector2 travelDirection;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private float damage = 0f;
    private float knockbackForce = 30f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator.SetFloat("XInput", 1);
        animator.SetFloat("YInput", 0);
        isDead = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // Move the fireball upwards
            rigidbody2d.velocity = travelDirection.normalized * speed;
            //(travelDirection.normalized * speed * Time.deltaTime);.MovePosition(rigidbody2d.position + travelDirection.normalized * speed * Time.deltaTime);
        }
        else{
            rigidbody2d.velocity = Vector2.zero;
        }


        timeAlive += Time.deltaTime;
        
        // Destroy the fireball when it goes off-screen transform.position.y > 10f || (did not work)
        if (timeAlive > maxTimeAlive)
        {
            PlayExplosion();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || other.CompareTag("Player")) return;
        // Destroy the fireball if it hits an enemy or other obstacle

        if(other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy)
            {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                Vector2 knockback = direction * knockbackForce;
                enemy.TakeDamage(damage, knockback);
            }
            PlayExplosion();
        }
        
        
    }

    private void PlayExplosion()
    {
        speed = 0;
        isDead = true;
        animator.SetTrigger("destroy");
    }
    public void DestroyFireball()
    {
        Destroy(gameObject);
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetKnockbackForce(float force)
    {
        knockbackForce = force;
    }
}
