using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EmotionSkill : Skill, ITargetedEnemySkill, IAttackSkill
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

        yield return new WaitForSeconds(5.5f);

        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attack));

        int mindam = 0;
        int maxdam = 0;

        for(int i = 0; i < skillContext.user.characterEmotion.imotionStack; i++)
        {
            mindam++;
            maxdam++;
        }

        SkillTemplet.Attack(this, MinDamage + mindam, MaxDamage + maxdam, skillContext);
        OnSkillEffected?.Invoke();

        yield return new WaitForSeconds(2f);
        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
