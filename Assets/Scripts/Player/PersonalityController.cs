using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New PersonalityController", menuName = "Personalities/PersonalityController")]
public class PersonalityController : ScriptableObject
{
    [SerializeField]
    public BaseAbility basicAbility;
    [SerializeField]
    public BaseAbility specialAbility;
    [SerializeField]
    public float moveSpeed = 2f;

    public void Init()
    {
        basicAbility.Init();
        specialAbility.Init();
    }
    public void ReduceCooldown()
    {
        basicAbility.ReduceCooldown();
        specialAbility.ReduceCooldown();
    }

}
