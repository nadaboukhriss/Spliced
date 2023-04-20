using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public float dirRight;
    public float dirUp;
    public float maxTimeAlive = 10f;

    private float timeAlive = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        timeAlive += Time.deltaTime;
        // Move the fireball upwards
        transform.Translate((dirRight * transform.right+ dirUp*Vector3.up) * speed * Time.deltaTime);
        // Destroy the fireball when it goes off-screen
        if (transform.position.y > 10f || timeAlive > maxTimeAlive)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the fireball if it hits an enemy or other obstacle
        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(2);
            }
            Destroy(gameObject);
        }
    }
}
