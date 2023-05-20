using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersonalityController2 : MonoBehaviour
{
    [Header("Common Personality Stats")]
    public float moveSpeed;
    [SerializeField]
    private float knockBackForce = 30f;

    public AbilityBase basicAbility;
    public AbilityBase specialAbility;

    protected Player player;
    protected PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        playerController = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    public abstract float GetBasicDamage();
    public float GetKnockbackForce()
    {
        return knockBackForce;
    }
    public abstract void BasicAbility();
    public abstract void SpecialAbility();

    // Update is called once per frame
    public void ReduceCooldown()
    {
        basicAbility.ReduceCooldown();
        specialAbility.ReduceCooldown();
    }
}
