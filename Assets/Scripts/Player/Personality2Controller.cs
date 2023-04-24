using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality2Controller : PersonalityController
{
    
    public override void BasicAttack()
    {
        if (!basicAttack.OnCooldown())
        {
            basicAttack.SetCooldown();
            player.GetComponent<Animator>().SetTrigger("swordAttack");
            GameObject fireballPrefab = Resources.Load<GameObject>("Fireball");
            GameObject fireballInstance = Instantiate(fireballPrefab);
            // Get the direction that the player is facing
            Vector2 direction = transform.right;

            fireballInstance.transform.position = transform.position;
            fireballInstance.GetComponent<Fireball>().dirRight = player.GetComponent<PlayerController>().dirRight;
            fireballInstance.GetComponent<Fireball>().dirUp = player.GetComponent<PlayerController>().dirUp;
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
