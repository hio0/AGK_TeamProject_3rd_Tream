using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Heal : Item
{
    public override void Effect()
    {
        float a = target.maxHp * 0.15f;

        target.Heal((int)a);
    }
}
