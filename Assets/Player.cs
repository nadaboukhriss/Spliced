using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private Image healthHeart;

    

    [SerializeField]
    private float looseTrustOnDeath = 0f;

    public int gameOverScreenSceneIndex = 2;

    public Inventory inventory;
    
    private float health;
    private Rigidbody2D rigidbody2d;

    private LevelSystem levelSystem;
    //private float xpChange = 0f;

    private void Awake()
    {
        health = maxHealth;
        healthHeart.fillAmount = 1;
        
    }
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        levelSystem = GetComponent<LevelSystem>();
        
    }
    public Player()
    {
        inventory = new Inventory();
    }


    public void ChangeTrust(float amount)
    {

        levelSystem.ChangeTrust(amount);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public float GetHealth()
    {
        return health;
    }
    public void UpdateHealthUI()
    {
        healthHeart.fillAmount = (float)health / maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthUI();

        if (health <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage, Vector2 knockback)
    {
        health -= damage;
        UpdateHealthUI();

        if (health <= 0)
        {
            Death();
        }
        else
        {
            rigidbody2d.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    private void RespawnPlayer()
    {
        rigidbody2d.position = GameManager.Instance.respawnPoint.position;
        Heal(maxHealth / 2);
        ChangeTrust(-looseTrustOnDeath);
    }
    private void Death()
    {
        RespawnPlayer();
        //SceneManager.LoadScene(gameOverScreenSceneIndex, LoadSceneMode.Single);
    }

    public void Heal(float amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthUI();
    }


}
