using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Skill : ScriptableObject
{
    public int cost;
    public SkillType type;
    public string skillName;
    public string description;
    public SkillState state = SkillState.Locked;
    public List<SkillType> conditions;
}


