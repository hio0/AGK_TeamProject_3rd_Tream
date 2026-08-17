using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillTemplet
{
    public static void Attack(IAttackSkill skill, int minDamage, int maxDamage, SkillContext skillContext) // 인터페이스에 함수는 쓸 수 있는데 상속 받아도 사용이 안되면 함수는 무슨 용도로 있는거야 난 인터페이스를 용서할 수 없어
    {
        foreach (Character target in skillContext.targets)
        {
            Action attackEffect = () =>
            {
                float damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
                Debug.Log($"BeforeDmg: {damage.ToString()}");

                AttackSkillData data = new AttackSkillData
                {
                    damage = damage,
                    minDamage = minDamage,
                    maxDamage = maxDamage
                };
                skill.OnAttack?.Invoke(data);
                target.hp -= (int)damage;
                Debug.Log($"AfterDmg: {damage.ToString()}");
            };

            target.Damaged(attackEffect);
        }
    }

    public static IconData FindIcon(List<IconData> skillIcons, Type iconType)
    {
        foreach (IconData iconData in skillIcons)
        {
            if (iconData.myIcon.GetType() == iconType)
            {
                return iconData;
            }
        }

        return null;
    }
}
