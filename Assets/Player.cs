using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private HealthBar healthBar;

    public int gameOverScreenSceneIndex = 2;

    public Inventory inventory;
    public float xp = 0f;
    private int health;
    //private float xpChange = 0f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    public Player()
    {
        inventory = new Inventory();
    }

    public void Update()
    {
    }

    public void ChangeXP(float change)
    {
        xp += change;
    }


    public int GetHealth()
    {
        return health;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        
        if (health <= 0)
        {
            Death();
        } else 
        {
            StopCoroutine(damageFlash());
            StartCoroutine(damageFlash());
        }
    }

    private void Death()
    {
        SceneManager.LoadScene(gameOverScreenSceneIndex, LoadSceneMode.Single);
    }

    public void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.SetHealth(health);
    }

    // Activates flashing when taking damage
    private IEnumerator damageFlash() {
        
        float ticks = 20f;
        for(int i = 0; i < ticks; i++) {
            float val = Mathf.Sin((Mathf.PI / ticks) * i);
            spriteRenderer.material.SetFloat("_Fade", val);

            yield return new WaitForSeconds(0.005f);
        }

        spriteRenderer.material.SetFloat("_Fade", 0f);
    }


}
