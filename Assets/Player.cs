using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private HealthBar healthBar;

    private int health;

    public Inventory inventory;

    private void Awake()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public Player()
    {
        inventory = new Inventory();
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


}
