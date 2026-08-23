using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public string defaultCharacterName;
    public List<SkillData> defaultSkillList = new();
    public SpeakData speakData;

    public Sprite iconImage;
    public Sprite standingImage;
    public Sprite hitImage;

    public int defaultHp;
    public int defaultMinSpeed;
    public int defaultMaxSpeed;
}
