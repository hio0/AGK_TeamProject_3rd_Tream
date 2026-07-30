using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("기본 정보")]
    public string skillName;
    public Sprite skillIcon;

    [TextArea] public string skillExplanation;

    public abstract void Effected(SkillContext skillContext);
}
