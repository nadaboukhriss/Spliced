using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public float dirRight;
    public float dirUp;
    public float maxTimeAlive = 10f;

    private float timeAlive = 0f;
    private bool isDead;

    public Vector2 travelDirection;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator.SetFloat("XInput", travelDirection.x);
        animator.SetFloat("YInput", travelDirection.y);
        isDead = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            // Move the fireball upwards
            rigidbody2d.MovePosition(rigidbody2d.position + travelDirection.normalized * speed * Time.deltaTime);
            //(travelDirection.normalized * speed * Time.deltaTime);
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
        if (other.CompareTag("Enemy") || (rigidbody2d.IsTouchingLayers() && !other.CompareTag("Player")))
        {
            Debug.Log("Fireball hit!");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(2);
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
}
