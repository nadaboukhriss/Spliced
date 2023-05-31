using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public static SkillTree Instance;

    [SerializeField]
    private GameObject skillPointUI;

    public Dictionary<SkillType,SkillUpgrade> SkillDict;
    public GameObject SkillHolder;

    public int SkillPoints;
    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SkillDict = new Dictionary<SkillType, SkillUpgrade>();
        foreach (var skill in SkillHolder.GetComponentsInChildren<SkillUpgrade>()) SkillDict[skill.id] = skill;
        UpdateAllSkillUI();
    }

    private void UpdateSkillPointUI()
    {
        if(SkillPoints <= 0)
        {
            skillPointUI.SetActive(false);
        }
        else
        {
            skillPointUI.SetActive(true);
            TMP_Text textBox = skillPointUI.GetComponentInChildren<TMP_Text>();
            textBox.text = SkillPoints.ToString();
        }
    }

    public void ChangeSkillPoints(int value)
    {

        SkillPoints += value;
        if (SkillPoints < 0) SkillPoints = 0;
        UpdateAllSkillUI();

    }
    public bool IsUnlocked(SkillType id)
    {
        return SkillDict[id].IsUnlocked();
    }
    public void UpdateAllSkillUI()
    {
        UpdateSkillPointUI();
        foreach (var skill in SkillDict.Values)
        {
            skill.UpdateUI();
        }
    }




}

public enum SkillState
{
    Locked,
    CanUnlock,
    Unlocked,
}

public enum SkillType
{
    Fireball0,
    Fireball1,
    Fireball2,
    Healing0,
    Healing1,
    Healing2,
}


