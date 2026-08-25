using System;

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
        target.OnAction -= Effect;
    }
}
