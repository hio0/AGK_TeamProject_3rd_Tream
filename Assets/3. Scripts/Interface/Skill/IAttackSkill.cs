using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkillData
{
    public int minDamage;
    public int maxDamage;
    public float damage;
}

public interface IAttackSkill
{
    int MinDamage { get; }
    int MaxDamage { get; }

    Action<AttackSkillData> OnAttack { get; set; }
}
