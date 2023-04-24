using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersonalityController : MonoBehaviour
{
    [SerializeField]
    public Ability basicAttack;
    [SerializeField]
    public Ability specialAbility;
    [SerializeField]
    public Ability switchCharacter;

    private List<Ability> abilities;
    protected GameObject player;

    public void Awake()
    {
        abilities = new List<Ability>();
        abilities.Add(basicAttack);
        abilities.Add(specialAbility);
        abilities.Add(switchCharacter);
    }
    public void Start()
    {
        player = GameManager.Instance.player;
        
    }
    public void SwitchCharacter()
    {
        if (!switchCharacter.OnCooldown())
        {
            switchCharacter.SetCooldown();
            player.GetComponent<SwapCharacters>().SwapCharacter();
        }
        
    }
    public abstract void BasicAttack();
    public abstract void SpecialAbility();

    protected void DecreaseCooldowns()
    {
        basicAttack.DecreaseCooldown();
        specialAbility.DecreaseCooldown();
        switchCharacter.DecreaseCooldown();
    }

    public List<Ability> GetAbilities()
    {
        return abilities;
    }

    public void Update()
    {
        DecreaseCooldowns();
    }

}
