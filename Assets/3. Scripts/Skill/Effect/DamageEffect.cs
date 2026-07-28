using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DamageEffect : SkillEffect
{
    public int damage;

    public override void Effect(SkillContext skillContext)
    {
        Debug.Log("dammage");
    }
}
