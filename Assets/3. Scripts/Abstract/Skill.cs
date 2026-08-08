using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public abstract class Skill : ScriptableObject
{
    [Header("기본 정보")]
    public string skillName;
    public Sprite skillIcon;
    public int skillTargetCount;

    [TextArea] public string skillExplanation;

    [Header("시스템")]
    public bool canTarget;

    public Action OnSkillStart;
    public Action OnSkillEffected;
    public Action OnSkillFinish;

    public abstract IEnumerator Effected(SkillContext skillContext); // context 받는 쪽이 계산 역할 !!

    public virtual bool CanCharacterTargeting(Character user, Character target) // 타겟팅에 조건 넣는 스킬들 위한 virtual 함수
    {
        DeffultTargetSeting(user, target);

        return canTarget;
    }

    void DeffultTargetSeting(Character user, Character target)
    {
        canTarget = false;

        switch (this)
        {
            case ITargetedOurSkill:
                if (user.iOurUnit == target.iOurUnit)
                {
                    canTarget = true;
                }
                break;
            case ITargetedEnemySkill:
                if (user.iOurUnit != target.iOurUnit)
                {
                    canTarget = true;
                }
                break;
            case ITargetedMeSkill:
                if (user.speed == target.speed)
                {
                    canTarget = true;
                }
                break;
        }
    }
}
