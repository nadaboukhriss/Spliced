using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private HealthBar healthBar;

    

    public Inventory inventory;
    public float xp = 0f;
    private int health;
    private float xpChange = 0f;

    private void Awake()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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


}
