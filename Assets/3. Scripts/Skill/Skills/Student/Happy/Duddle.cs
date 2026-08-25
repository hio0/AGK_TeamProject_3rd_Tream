using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Duddle : Skill, ITargetedEnemySkill, IAttackSkill
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
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.buff));
        yield return new WaitForSeconds(0.5f);

        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attackReady));

        yield return new WaitForSeconds(1f);

        SkillTemplet.Attack(this, MinDamage, MaxDamage, skillContext);

        foreach(Character our in skillContext.ourRangeChar)
        {
            int r = UnityEngine.Random.Range(1, 3);

            our.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Deffence)), r);
        }

        foreach(Character ene in skillContext.enemyRangeChar)
        {
            int r = UnityEngine.Random.Range(2, 4);

            ene.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Broken)), r);
        }

        OnSkillEffected?.Invoke();

        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attack));

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
