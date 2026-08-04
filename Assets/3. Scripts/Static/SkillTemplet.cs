using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillTemplet
{
    public static void Attack(int minDamage, int maxDamage, SkillContext skillContext) // 인터페이스에 함수는 쓸 수 있는데 상속 받아도 사용이 안되면 함수는 무슨 용도로 있는거야 난 인터페이스를 용서할 수 없어
    {
        foreach (Character target in skillContext.targets)
        {
            Action attackEffect = () =>
            {
                int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
                target.hp -= damage;
            };

            target.Damaged(attackEffect);
        }
    }
}
