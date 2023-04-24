using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoxBasicAbility", menuName = "Abilities/FoxBasicAbility")]
public class FoxBasicAbility : BaseAbility
{
    [SerializeField]
    private float damage;
    public override void Use()
    {
        StartCooldown();
        player.GetComponent<Animator>().SetTrigger("swordAttack");
        GameObject fireballPrefab = Resources.Load<GameObject>("Fireball");
        GameObject fireballInstance = Instantiate(fireballPrefab);
        // Get the direction that the player is facing
        Vector2 direction = player.transform.right;

        fireballInstance.transform.position = player.transform.position;
        fireballInstance.GetComponent<Fireball>().dirRight = player.GetComponent<PlayerController>().dirRight;
        fireballInstance.GetComponent<Fireball>().dirUp = player.GetComponent<PlayerController>().dirUp;
    }
}
