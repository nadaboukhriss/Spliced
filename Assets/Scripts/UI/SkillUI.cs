using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text TitleText;
    [SerializeField]
    private TMP_Text DescText;
    
    public void SetValues(string title, string desc,int cost)
    {
        TitleText.text = title;
        DescText.text = desc;
    }

}
