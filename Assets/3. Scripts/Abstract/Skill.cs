using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[Serializable]
public abstract class Skill
{
    public SkillData data { get; private set; }

    [Header("시스템")]
    public bool canTarget;

    public Action<SkillData> OnSkillAction;
    public Action OnSkillStart;
    public Action OnSkillEffected;
    public Action OnSkillFinish;

    public void Initialize(SkillData data)
    {
        this.data = data;
    }

    public abstract IEnumerator Effected(SkillContext skillContext); // context 받는 쪽이 계산 역할 !!

    public virtual bool CanCharacterTargeting(Character user, Character target) // 타겟팅에 조건 넣는 스킬들 위한 virtual 함수
    {
        DeffultTargetSeting(user, target);

        return canTarget;
    }

    protected virtual SkillData ReturnData()
    {
        SkillData data = new SkillData
        {
            skillType = this.data.skillType,
            skillName = this.data.skillName,
            skillCoolTime = this.data.skillCoolTime,
            skillExplanation = this.data.skillExplanation,
            skillIcons = this.data.skillIcons,
            skillTargetCount = this.data.skillTargetCount
        };

        return data;
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
                if (user.nowTurnCount == target.nowTurnCount)
                {
                    canTarget = true;
                }
                break;
        }
    }
}
