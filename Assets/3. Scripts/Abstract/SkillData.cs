using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeReference, SubclassSelector] public Skill mySkill;
    public enum actType
    {
        attack,
        guard,
        special,
        emotion
    }
    public actType skillType;
    public string skillName;
    public int skillTargetCount;
    public int skillCoolTime;

    public List<IconData> skillIcons;

    [TextArea] public string skillExplanation;
}
