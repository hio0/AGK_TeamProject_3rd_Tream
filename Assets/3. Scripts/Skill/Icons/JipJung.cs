using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JipJung : Icon
{
    public IconData surprised;

    protected override void EffectTerms()
    {
        FightManager.Instance.OnActingFinished += Effect;
    }

    protected void Effect()
    {
        List<Character> list = FightManager.Instance.GetRangeData.Invoke().ourRangeChar;

        foreach (Character character in list)
        {
            float heal = character.maxHp * 0.15f;

            character.Heal((int)heal);
        }
    }

    protected override void RemoveTerms()
    {
        target.OnDamaged += Remove;
    }

    void Remove()
    {
        RemoveEvent();
        target.AddIcon(surprised, 2);
        target.RemoveIcon(this);
    }

    public override void RemoveEvent()
    {
        FightManager.Instance.OnActingFinished -= Effect;
        target.OnDamaged -= Remove;
    }
}
