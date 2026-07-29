using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    [Header("기본 정보")]
    public SkillData skillData;

    public string skillName;
    public Sprite skillIcon;
    public string skillExplanation;

    public Skill()
    {
        skillName = skillData.skillName;
        skillIcon = skillData.skillIcon;
        skillExplanation = skillData.skillExplanation;
    }

    public void Use(SkillContext skillContext)
    {
        foreach(SkillEffect effect in skillData.effects)
        {
            effect.Effect(skillContext);
        }
    }
}
