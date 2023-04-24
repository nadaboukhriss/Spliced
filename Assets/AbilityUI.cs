using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{

    [SerializeField]
    private Image imageCooldown;

    // Start is called before the first frame update

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
