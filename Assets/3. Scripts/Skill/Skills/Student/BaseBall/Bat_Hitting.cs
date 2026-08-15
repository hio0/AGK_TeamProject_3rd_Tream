using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skils/BatHitting")]
public class Bat_Hitting : Skill, ITargetedEnemySkill, IAttackSkill
{
    public int minDamage;
    public int maxDamage;

    public int MinDamage => minDamage;
    public int MaxDamage => maxDamage;

    public Action<AttackSkillData> OnAttack { get; set; }

    public override IEnumerator Effected(SkillContext skillContext)
    {
        OnSkillAction?.Invoke(ReturnData(this));

        OnSkillStart?.Invoke();
        yield return new WaitForSeconds(1f);

        SkillTemplet.Attack(this, MinDamage, MaxDamage, skillContext);
        OnSkillEffected?.Invoke();

        skillContext.user.AddIcon(SkillTemplet.FindIcon(skillIcons, typeof(Power)), skillContext, 3);

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
