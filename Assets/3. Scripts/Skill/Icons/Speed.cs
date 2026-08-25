using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Speed : Icon
{
    protected override void EffectTerms()
    {
        target.OnSpeed += Effect;
    }

    protected void Effect(int speed)
    {
        target.speed += stack;
        RemoveEvent();
        target.RemoveIcon(this);
    }

    protected override void RemoveTerms()
    {
        
    }

    public override void RemoveEvent()
    {
        target.OnSpeed -= Effect;
    }
}
