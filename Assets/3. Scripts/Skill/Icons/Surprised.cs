using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Surprised : Icon
{
    protected override void EffectTerms()
    {
        
    }

    protected void Effect(int speed)
    {
        
    }

    protected override void RemoveTerms()
    {
        FightManager.Instance.OnTurnFinish += IsRemoveIcon;
    }

    public override void RemoveEvent()
    {
        FightManager.Instance.OnTurnFinish -= IsRemoveIcon;
    }
}
