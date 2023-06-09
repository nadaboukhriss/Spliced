using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private Image healthHeart;

    [SerializeField]
    private GameObject fadeScreenUI;

    

    [SerializeField]
    private float looseTrustOnDeath = 0f;

    public int gameOverScreenSceneIndex = 2;

    public Inventory inventory;
    
    private float health;
    private Rigidbody2D rigidbody2d;

    private LevelSystem levelSystem;
    //private float xpChange = 0f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = maxHealth;

        spriteRenderer = transform.GetComponent<SpriteRenderer>();
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
        }else{
            StopCoroutine(damageFlash());
            StartCoroutine(damageFlash());
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
            StopCoroutine(damageFlash());
            StartCoroutine(damageFlash());
            rigidbody2d.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    private void RespawnPlayer()
    {
        GameManager.Instance.fadeScreen.StartFade(GameManager.Instance.respawnPoint.position);
        Heal(maxHealth);
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

    IEnumerator ActivateAndTeleport()
    {
    // // GameObject prefab = Resources.Load<GameObject>("FadeInFadeOut");
        //fadeInFadeOutElt = Instantiate(prefab);
        //fadeInFadeOutElt.SetActive(true); // First line
        Animator animator = fadeScreenUI.GetComponent<Animator>();
        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(0.8f); // Wait for 1 second

        transform.position = GameManager.Instance.respawnPoint.position; // Second line

        yield return new WaitForSeconds(0.25f); // Wait for 1 second
        animator.SetTrigger("FadeOut"); // Third line

        // fadeInFadeOutElt.SetActive(false);
        //Destroy(fadeInFadeOutElt);
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
