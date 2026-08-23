using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hit : Skill, ITargetedEnemySkill, IAttackSkill
{
    public int minDamage;
    public int maxDamage;

    public int MinDamage => minDamage;
    public int MaxDamage => maxDamage;

    public Func<AttackSkillData, AttackSkillData> OnAttack { get; set; }

    public override IEnumerator Effected(SkillContext skillContext)
    {
        OnSkillAction?.Invoke(ReturnData());

        OnSkillStart?.Invoke();
        yield return new WaitForSeconds(1f);

        SkillTemplet.Attack(this, MinDamage, MaxDamage, skillContext);
        OnSkillEffected?.Invoke();

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();

        FightManager.Instance.OnActingFinished?.Invoke();
    }
}
