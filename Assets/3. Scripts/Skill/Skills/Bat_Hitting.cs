using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skils/BatHitting")]
public class Bat_Hitting : Skill, ITargetedEnemySkill, IAttackSkill
{
    public int damage { get; set; }

    public event Action OnAttack;

    public override void Effected(SkillContext skillContext)
    {
        SkillTemplet.Attack(this, damage, skillContext);
        OnAttack?.Invoke();
    }
}
