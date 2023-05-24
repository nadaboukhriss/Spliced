using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : PersonalityController2
{
    [Header("Sword Attack")]
    [SerializeField]
    private float swordDamage = 5f;

    [Header("Dashing")]
    [SerializeField]
    private float dashDuration = 0.15f;
    [SerializeField]
    private float dashForce = 1000;
    public override void BasicAbility()
    {
        basicAbility.Activate();
        playerController.GetComponent<Animator>().SetTrigger("swordAttack");
        //SFX
        //Should modify hitbox of hurtbox when this is playing, is done in animator
        Debug.Log("Human sword attack, Damage: " + swordDamage.ToString());
    }

    public override float GetBasicDamage()
    {
        return swordDamage;
    }

    public override void SpecialAbility()
    {
        specialAbility.Activate();
        playerController.rigidbody2d.AddForce(playerController.GetLastOrientering() * dashForce,ForceMode2D.Impulse);
    }

    private void StopSpecialAbility()
    {
        //playerController.rigidbody2d.velocity = Vector2.zero;
        playerController.SetBoosting(false);
    }
}
