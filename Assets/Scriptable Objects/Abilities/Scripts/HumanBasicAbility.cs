using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanBasicAbility", menuName = "Abilities/HumanBasic")]
public class HumanBasicAbility : BaseAbility
{
    [SerializeField]
    private float damage;
    public override void Use()
    {
        StartCooldown();
        player.GetComponent<Animator>().SetTrigger("swordAttack");
        LayerMask enemyLayers = LayerMask.GetMask("Enemy");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.transform.position, 2f, enemyLayers);
        foreach (Collider2D ene in hitEnemies)
        {
            GameObject enemyGameObject = ene.gameObject;
            Enemy enemyComponent = enemyGameObject.GetComponent<Enemy>();

            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(damage);
            }
        }
    }
}
