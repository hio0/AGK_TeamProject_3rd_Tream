using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public string defaultCharacterName;
    public List<Skill> defaultSkillList = new();

    public int defaultHp;
    public int defaultMinSpeed;
}
