using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoxSpecialAbility", menuName = "Abilities/FoxSpecial")]
public class FoxSpecialAbility : BaseAbility
{
    [SerializeField]
    private int healAmount;
    public override void Use()
    {
        StartCooldown();
        player.Heal(healAmount);
        //Debug.Log("Fox using special ability, healing "+ healAmount);
    }
}
