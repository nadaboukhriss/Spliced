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
    [SerializeField]
    private HealAnimation healScript;
    public override void BasicAbility()
    {
        basicAbility.Activate();
    }

    public override void SpecialAbility()
    {
        specialAbility.Activate();
        healScript.Activate();
        
    }
}
