using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dummy : Item
{
    protected override void EffectTerms()
    {
        target.OnActingStart += Effect;
    }

    protected void Effect()
    {
        target.AddIcon(SkillTemplet.FindIcon(data.icons, typeof(Power)), 2);

        OnItemEffected?.Invoke(this);
    }

    public override void Remove()
    {
        target.OnActingStart -= Effect;
    }
}
