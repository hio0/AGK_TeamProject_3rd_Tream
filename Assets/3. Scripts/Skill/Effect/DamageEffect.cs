using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DamageEffect : SkillEffect
{
    int damage { get; set; }

    public int minDamage;
    public int maxDamage;

    public override void Effect(SkillContext skillContext)
    {
        int currentDamage = UnityEngine.Random.Range(minDamage, maxDamage + 1);

        Debug.Log($"dammage: {damage}");
    }
}
