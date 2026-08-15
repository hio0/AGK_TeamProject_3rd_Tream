using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IconContext
{
    public IconData data;
    public Character target;
    public Skill skill;
}

[Serializable]
public abstract class Icon
{
    public IconData data;
    public Character target;
    public Skill skill;
    public int stack;
    public int remainTime;

    public void Initialize(IconContext context)
    {
        data = context.data;
        target = context.target;
        skill = context.skill;

        stack = 1;
        remainTime = data.limitTurn;

        EffectTerms();
        RemoveTerms();
    }

    public void ChangeStack(int stack)
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
    protected abstract void RemoveEvent();

    // 템플릿
    protected void ActionEffect(IconContext context, Action<AttackSkillData> effect)
    {
        if (context.skill is IAttackSkill)
        {
            IAttackSkill atk = context.skill.GetComponent<IAttackSkill>();

            atk.OnAttack += effect;
        }
    }

    public void IsRemoveIcon()
    {
        remainTime--;

        if(remainTime <= 0)
        {
            target.RemoveIcon(this);
        }
    }
}
