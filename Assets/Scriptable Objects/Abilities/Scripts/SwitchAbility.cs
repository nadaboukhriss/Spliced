using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SwitchAbility", menuName = "Abilities/SwitchAbility")]
public class SwitchAbility : BaseAbility
{
    public override void Use()
    {
        StartCooldown();
        player.GetComponent<SwapCharacters>().SwapCharacter();
    }
}
