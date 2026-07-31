using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillTemplet
{
    public static void Attack(IAttackSkill atkSkill, int damage, SkillContext skillContext)
    {
        atkSkill.Attack(damage, skillContext);
    }
}
