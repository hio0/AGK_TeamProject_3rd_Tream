using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bomb : Skill, ITargetedEnemySkill, IAttackSkill
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

        yield return new WaitForSeconds(1.5f);

        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.buff));

        SkillTemplet.Attack(this, MinDamage, MaxDamage, skillContext);

        foreach (Character ene in skillContext.enemyRangeChar)
        {
            int r = UnityEngine.Random.Range(2, 4);

            ene.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(NoRun)), r);
            ene.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Broken)), 4);
        }

        OnSkillEffected?.Invoke();

        yield return new WaitForSeconds(0.5f);

        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
