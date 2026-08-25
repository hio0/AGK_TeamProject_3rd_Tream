using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Broken : Icon
{
    protected override void EffectTerms()
    {
        FightManager.Instance.OnTargetFinded += Effect;
    }

    protected override void Effect(SkillContext context)
    {
        if (context.targets.Contains(target) && context.useSkill is IAttackSkill)
        {
            IAttackSkill atk = (IAttackSkill)context.useSkill;
            AttackSkillData data = new();
            Func<AttackSkillData, AttackSkillData> act = (atkdata) =>
            {
                data = atkdata;

                data.damage *= 1f + 0.1f * stack;

                return data;
            };
            atk.OnAttack += act;
        }
    }

    protected override void RemoveTerms()
    {
        FightManager.Instance.OnTurnFinish += IsRemoveIcon;
    }

    public override void RemoveEvent()
    {
        FightManager.Instance.OnTurnFinish -= IsRemoveIcon;
        FightManager.Instance.OnTargetFinded -= Effect;
    }
}
