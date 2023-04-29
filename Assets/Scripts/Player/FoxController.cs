using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : PersonalityController2
{
    [Header("Fireball")]
    [SerializeField]
    private float fireballDamage;

    [Header("Healing")]
    [SerializeField]
    private int healAmount;
    public override void BasicAbility()
    {
        Debug.Log("Fox basic ability");
        basicAbility.Activate();
        GameObject fireballPrefab = Resources.Load<GameObject>("Fireball");
        GameObject fireballInstance = Instantiate(fireballPrefab);
        // Get the direction that the player is facing
        Vector2 direction = player.transform.right;

        fireballInstance.transform.position = player.transform.position;
        fireballInstance.GetComponent<Fireball>().dirRight = player.GetComponent<PlayerController>().dirRight;
        fireballInstance.GetComponent<Fireball>().dirUp = player.GetComponent<PlayerController>().dirUp;
    }

    public override void SpecialAbility()
    {
        specialAbility.Activate();
        player.Heal(healAmount);
    }
}
