using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float damage;

    public AudioClip HitSound; // Can be played whenever something is hit (with sword/fireball)
    public AudioClip DeathSound;

    protected float health;

    private bool isAlive = true;
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    private EnemyAIAstar ai;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAIAstar>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }



    public void TakeDamage(float damage)
    {
        health -= damage;
        health = health < 0 ? 0 : health;
        //healthbar.transform.localScale = new Vector3(health/maxHealth,healthbar.transform.localScale.y,healthbar.transform.localScale.z);
        
        

        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            ai.SetState(EnemyState.Dead);
            
            // SFX
            if (DeathSound != null)
            {
                SFXManager.sfxinstance.Audio.PlayOneShot(DeathSound);
            }
        } else {

            StopCoroutine(damageFlash());
            StartCoroutine(damageFlash());
        
            // SFX
            if (HitSound != null)
            {
                SFXManager.sfxinstance.Audio.PlayOneShot(HitSound);
            }
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
