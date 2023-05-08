using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float maxTimeAlive = 10f;

    private float timeAlive = 0f;
    private bool isDead;

    public Vector2 travelDirection;
    private Animator animator;
    private Rigidbody2D rigidbody2d;


    private float damage = 0f;

    public void SetDamage(float dmg)
    {
        damage = dmg;
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
            rigidbody2d.MovePosition(rigidbody2d.position + travelDirection.normalized * speed * Time.deltaTime);
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
        // Destroy the fireball if it hits an enemy or other obstacle
        if (other.CompareTag("Player") || (other.CompareTag("Obstacle")))
        {
            Player player = other.GetComponentInParent<Player>();
            if (player)
            {

                player.TakeDamage((int)damage);
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
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
