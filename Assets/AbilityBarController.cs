using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbilityBarController : MonoBehaviour
{

    private List<AbilityUI> abilities;
    private SwapCharacters player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player.GetComponent<SwapCharacters>();
        abilities = new List<AbilityUI>();
        foreach (AbilityUI ability in GetComponentsInChildren<AbilityUI>())
        {
            abilities.Add(ability);
        }
        
    }

    public void Update()
    {
        List<Ability> abilitiesData = player.GetCurrentPersonality().GetAbilities();
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].SetFillAmount(abilitiesData[i].GetPercentageLeft());
        }
    }
}
