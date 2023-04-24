using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality1Controller : PersonalityController
{
    
    public override void BasicAttack()
    {
        if (!basicAttack.OnCooldown())
        {
            basicAttack.SetCooldown();
            player.GetComponent<Animator>().SetTrigger("swordAttack");
            LayerMask enemyLayers = LayerMask.GetMask("Enemy");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 2f, enemyLayers);
            foreach (Collider2D ene in hitEnemies)
            {
                print("Hello there enemy!");
                GameObject enemyGameObject = ene.gameObject;
                Enemy enemyComponent = enemyGameObject.GetComponent<Enemy>();

                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(basicAttack.GetDamage());
                }
            }
        }
        
    }

    public override void SpecialAbility()
    {
        if (!specialAbility.OnCooldown())
        {
            specialAbility.SetCooldown();
        }
    }
}
