using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxTimeAlive = 10f;
    private float knockbackForce = 10f;
    private float timeAlive = 0f;
    private bool isDead;

    private Vector2 travelDirection;
    private Animator animator;
    private Rigidbody2D rigidbody2d;


    private float damage = 0f;

    public void SetParameters(float dmg,float knockback, Vector2 movement)
    {
        this.damage = dmg;
        this.knockbackForce = knockback;
        this.travelDirection = movement;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            rigidbody2d.MovePosition(rigidbody2d.position + travelDirection * Time.deltaTime);
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
        if (isDead) return;
        if (other.CompareTag("Player") || (other.CompareTag("Obstacle")))
        {
            Player player = other.GetComponentInParent<Player>();
            if (player)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                Vector2 knockback = direction * knockbackForce;
                player.TakeDamage((int)damage, knockback);
            }
            PlayExplosion();
        }
    }

    private void PlayExplosion()
    {
        travelDirection = Vector2.zero;
        isDead = true;
        animator.SetTrigger("destroy");
    }
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
