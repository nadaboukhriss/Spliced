using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float knockbackForce;
    [SerializeField]
    private float trustGain = 10f;

    public AudioSource HitSound; // Can be played whenever something is hit (with sword/fireball)
    public AudioSource DeathSound;

    protected float health;

    private bool isAlive = true;
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    private Transform healthBar;
    private EnemyAIAstar ai;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAIAstar>();
        healthBar = transform.Find("EnemyHealthbar").GetComponent<Transform>();
        UpdateHealthbar(1);
    }

    private void UpdateHealthbar(float xScale)
    {
        healthBar.localScale = new Vector3(xScale, healthBar.localScale.y, healthBar.localScale.z);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    public float GetKnockbackForce()
    {
        return knockbackForce;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        health = health < 0 ? 0 : health;
        UpdateHealthbar(health/maxHealth);
        if (health <= 0)
        {
            Death();
        } else {

            StopCoroutine(damageFlash());
            StartCoroutine(damageFlash());
        
            // SFX
            if (HitSound != null)
            {
                IEnumerator coroutine = PlayAudio(HitSound.clip);
                StartCoroutine(coroutine);
            }
        }
    }

    public IEnumerator PlayAudio(AudioClip clip)
    {
        SFXManager.sfxinstance.Audio.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
    }
    public void TakeDamage(float damage, Vector2 knockback)
    {
        health -= damage;
        health = health < 0 ? 0 : health;
        UpdateHealthbar(health / maxHealth);
        ai.SearchForPlayer();
        rigidbody2d.AddForce(knockback, ForceMode2D.Impulse);

        if (health <= 0)
        {
            Death();
            
        } else {

            StopCoroutine(damageFlash());
            StartCoroutine(damageFlash());
        
            // SFX
            if (HitSound != null)
            {
                IEnumerator coroutine = PlayAudio(HitSound.clip);
                StartCoroutine(coroutine);
            }
        }
    }
    private void Death()
    {
        GameManager.Instance.player.GetComponent<Player>().ChangeTrust(trustGain);
        animator.SetTrigger("Dead");
        ai.SetState(EnemyState.Dead);
        
        // SFX
        if (DeathSound != null)
        {
            IEnumerator coroutine = PlayAudio(DeathSound.clip);
            StartCoroutine(coroutine);
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
    public void Defeated()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    protected virtual void Attack()
    {
        
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
