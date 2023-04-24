using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoxSpecialAbility", menuName = "Abilities/FoxSpecial")]
public class FoxSpecialAbility : BaseAbility
{
    [SerializeField]
    private float healAmount;
    public override void Use()
    {
        StartCooldown();
        Debug.Log("Fox using special ability, healing "+ healAmount);
    }
}
