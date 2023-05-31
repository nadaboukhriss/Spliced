using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeSequenceUI : MonoBehaviour
{
    [SerializeField]
    private GameObject skillPrefab;
    [SerializeField]
    private GameObject connectorPrefab;

    public void Initalize(List<Skill> skills)
    {
        for(int i = 0; i < skills.Count; i++)
        {
            Skill skill = skills[i];
            GameObject instance = Instantiate(skillPrefab, gameObject.transform);


            instance.GetComponent<SkillUI>().SetValues(skill.skillName, skill.description, skill.cost);

        }
    }
}
