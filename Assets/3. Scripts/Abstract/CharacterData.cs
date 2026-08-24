using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MotionData
{
    public enum MotionType
    {
        standing,
        run,
        hit,
        attackReady,
        attack,
        buff,

    }

    public MotionType type;
    public Sprite image;
}

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public string defaultCharacterName;
    public List<SkillData> defaultSkillList = new();
    public List<MotionData> motionData = new();
    public SpeakData speakData;

    public Sprite iconImage;
    public Sprite standingImage;
    public Sprite hitImage;

    public int defaultHp;
    public int defaultMinSpeed;
    public int defaultMaxSpeed;
}
