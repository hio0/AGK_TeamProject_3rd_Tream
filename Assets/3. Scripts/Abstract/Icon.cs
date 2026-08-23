using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IconContext
{
    public IconData data;
    public Character target;
}

[Serializable]
public abstract class Icon
{
    public IconData data;
    public Character target;
    public int stack;
    public int remainTime;

    public void Initialize(IconContext context)
    {
        data = context.data;
        target = context.target;

        stack = 0;
        remainTime = data.limitTurn;

        EffectTerms();
        RemoveTerms();
    }

    public virtual void ChangeStack(int stack)
    {
        int plused = this.stack + stack;

        if (plused > data.limitStack)
        {
            plused = data.limitStack;
        }

        this.stack = plused;
        remainTime = data.limitTurn;
    }

    // 자식꺼
    protected virtual void Effect(SkillContext context)
    { }
    protected abstract void EffectTerms();
    protected abstract void RemoveTerms();
    public abstract void RemoveEvent();

    // 템플릿
    protected void ActionEffect(IAttackSkill skill, Func<AttackSkillData, AttackSkillData> effect)
    {
        skill.OnAttack += effect;
    }

    public void IsRemoveIcon()
    {
        remainTime--;

        if (remainTime <= 0)
        {
            target.RemoveIcon(this);
        }
    }
}
