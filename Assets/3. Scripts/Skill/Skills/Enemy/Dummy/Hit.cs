using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skils/Hit")]
public class Hit : Skill, ITargetedEnemySkill, IAttackSkill
{
    public int minDamage;
    public int maxDamage;

    public int MinDamage => minDamage;
    public int MaxDamage => maxDamage;

    public event Action OnAttack;

    public override IEnumerator Effected(SkillContext skillContext)
    {
        OnSkillStart?.Invoke();
        yield return new WaitForSeconds(1f);

        SkillTemplet.Attack(MinDamage, MaxDamage, skillContext);
        OnSkillEffected?.Invoke();
        OnAttack?.Invoke();

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();

        FightManager.Instance.OnActingFinished?.Invoke();
    }
}
