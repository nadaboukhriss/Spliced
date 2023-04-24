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
    private List<AbilityUI> abilities;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    public void Update()
    {
        List<BaseAbility> currentAbilities = player.GetCurrentAbilities();
        basicAbility.SetFillAmount(currentAbilities[0].GetPercentageLeft());
        specialAbility.SetFillAmount(currentAbilities[1].GetPercentageLeft());

        switchAbility.SetFillAmount(player.GetSwitchAbility().GetPercentageLeft());
    }
}
