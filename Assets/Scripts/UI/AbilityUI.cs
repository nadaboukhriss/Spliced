using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{

    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private Image abilityIcon;
    // Start is called before the first frame update

    public void SetAbilityIcon(Sprite icon)
    {
        abilityIcon.sprite = icon;
    }
    public void SetFillAmount(float value)
    {
        if(value <= 0)
        {
            imageCooldown.enabled = false;
            imageCooldown.fillAmount = 0;
        }
        else
        {
            imageCooldown.enabled = true;
            imageCooldown.fillAmount = value;
        }
    }
}
