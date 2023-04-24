using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanSpecialAbility", menuName = "Abilities/HumanSpecial")]
public class HumanSpecialAbility : BaseAbility
{
    [SerializeField]
    private float dashDistance;
    public override void Use()
    {
        Debug.Log("Human doing special ability");
    }
}
