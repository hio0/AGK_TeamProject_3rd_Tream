using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Yeek : Skill, ITargetedEnemySkill, IAttackSkill
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
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attackReady));

        yield return new WaitForSeconds(1f);

        int mindam = MinDamage;
        int maxdam = MaxDamage;

        if(skillContext.user.iconlist.OfType<Surprised>().Any())
        {
            mindam = MinDamage + 4;
            maxdam = MaxDamage - 1;
        }

        SkillTemplet.Attack(this, mindam, maxdam, skillContext);
        OnSkillEffected?.Invoke();

        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attack));

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
