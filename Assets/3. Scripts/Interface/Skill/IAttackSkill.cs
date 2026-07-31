using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackSkill
{
    int damage { get; set; }

    event Action OnAttack;

    public void Attack(int damage, SkillContext skillContext)
    {
        foreach(Character target in skillContext.targets)
        {
            Action attackEffect = () =>
            {
                target.hp -= damage;
            };

            target.Damaged(attackEffect);
        }
    }
}
