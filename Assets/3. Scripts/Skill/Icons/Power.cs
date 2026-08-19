using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[Serializable]
public class Power : Icon
{
    protected override void EffectTerms()
    {
        target.OnAction += Effect;
    }

    protected override void Effect(SkillContext context)
    {
        if(context.useSkill is IAttackSkill)
        {
            IAttackSkill atk = (IAttackSkill)context.useSkill;
            AttackSkillData data = new();
            Func<AttackSkillData, AttackSkillData> act = (atkdata) =>
            {
                data = atkdata;

                float power = data.damage / 10 * stack;
                data.damage *= power;

                return data;
            };
            atk.OnAttack += act;
            atk.OnAttack -= act;
        }
    }

    protected override void RemoveTerms()
    {
        FightManager.Instance.OnTurnFinish += IsRemoveIcon;
    }

    public override void RemoveEvent()
    {
        FightManager.Instance.OnTurnFinish -= IsRemoveIcon;
        target.OnAction -= Effect;
    }
}
