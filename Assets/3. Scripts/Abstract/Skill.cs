using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("기본 정보")]
    public string skillName;
    public Sprite skillIcon;
    public int skillTargetCount;

    [TextArea] public string skillExplanation;

    [Header("시스템")]
    public bool canTarget;

    public event Action OnSkillStart;
    public event Action OnSkillEffected;
    public event Action OnSkillFinish;

    public abstract void Effected(SkillContext skillContext); // context 받는 쪽이 계산 역할 !!

    public virtual bool CanCharacterTargeting(Character character) // 타겟팅에 조건 넣는 스킬들 위한 virtual 함수
    {
        canTarget = true;

        return canTarget;
    }
}
