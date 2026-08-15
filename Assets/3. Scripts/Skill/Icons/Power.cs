using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Power : Icon
{
    /*
    public override void Subs(IconContext context)
    {
        context.user.OnAction += ActionEffect;

        void ActionEffect(SkillContext contexts)
        {
            if (contexts.useSkill is IAttackSkill)
            {
                IAttackSkill atk = contexts.useSkill.GetComponent<IAttackSkill>();

                atk.OnAttack += Effect;
            }

            void Effect(AttackSkillData data)
            {
                float power = data.damage / 10 * context.stack;
                data.damage *= power;
            }
        }
    }

    public override void RemoveIcon(IconContext context)
    {
        FightManager.Instance.OnTurnFinish += context.icon.IsRemoveIcon;
    }

    public override void RemoveEvent(IconContext context)
    {
        FightManager.Instance.OnTurnFinish -= context.icon.IsRemoveIcon;
    }
    */
}
