using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    [Header("기본 정보")]
    public Skill skill;
    public string skillName;
    public Sprite skillIcon;

    [TextArea] public string skillExplanation;

    [SerializeReference] public List<SkillEffect> effects;
}
