using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbilityBarController : MonoBehaviour
{

    [SerializeField]
    private AbilityUI basicAbility;
    [SerializeField]
    private AbilityUI specialAbility;
    [SerializeField]
    private AbilityUI switchAbility;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    public void Update()
    {
        basicAbility.SetFillAmount(player.GetBasicAbility().GetCooldownFraction());
        basicAbility.SetAbilityIcon(player.GetBasicAbility().abilityIcon);

        specialAbility.SetFillAmount(player.GetSpecialAbility().GetCooldownFraction());
        specialAbility.SetAbilityIcon(player.GetSpecialAbility().abilityIcon);

        switchAbility.SetFillAmount(player.GetSwitchAbility().GetCooldownFraction());
        switchAbility.SetAbilityIcon(player.GetSwitchAbility().abilityIcon);
    }
}
