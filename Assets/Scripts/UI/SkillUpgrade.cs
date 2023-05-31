using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillUpgrade : MonoBehaviour
{

    public SkillType id;

    [SerializeField]
    private bool firstInSequence;

    [SerializeField]
    private SkillState state = SkillState.Locked;

    [SerializeField]
    private TMP_Text TitleText;
    [SerializeField]
    private TMP_Text DescText;
    [SerializeField]
    private GameObject connector;

    [SerializeField]
    private List<SkillType> conditions;

    [SerializeField]
    private int cost = 1;


    private Button button;

    public void Start()
    {
        
        if (firstInSequence)
        {
            connector.SetActive(false);
        }
    }

    public void UpdateUI()
    {
        if (IsUnlockable() && state == SkillState.Locked) state = SkillState.CanUnlock;
        Image img = GetComponent<Image>();
        button = GetComponent<Button>();
        Image connectorImg = connector.GetComponent<Image>();
        switch (state)
        {
            case SkillState.Locked:
                button.enabled = false;
                img.color = new Color(0.5f, 0.5f, 0.5f);
                connectorImg.color = new Color(0.5f, 0.5f, 0.5f);
                break;
            case SkillState.CanUnlock:
                button.enabled = true;
                Color c = new Color(1, 1, 1);
                c.a = 0.7f;
                img.color = c;
                c = connectorImg.color;
                c.a = 0.7f;
                connectorImg.color = c;
                break;
            case SkillState.Unlocked:
                button.enabled = false;
                img.color = new Color(1, 1, 1);
                break;
        }
    }

    public bool IsUnlocked()
    {
        return state == SkillState.Unlocked;
    }
    private bool IsUnlockable()
    {
        foreach(var condition in conditions)
        {
            if (!SkillTree.Instance.IsUnlocked(condition))
            {
                return false;
            }
        }
        return SkillTree.Instance.SkillPoints >= cost;
    }

    public void Unlock()
    {
        if (IsUnlockable())
        {
            SkillTree.Instance.ChangeSkillPoints(-cost);
            state = SkillState.Unlocked;
            SkillTree.Instance.UpdateAllSkillUI();
        }
    }
}




