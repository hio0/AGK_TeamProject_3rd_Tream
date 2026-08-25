using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Bat_Hitting : Skill, ITargetedEnemySkill, IAttackSkill
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
        
        FocusCamera.Instance.LockingMovingCamera(false);

        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attackReady));

        yield return new WaitForSeconds(0.7f);
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attack));

        SkillTemplet.Attack(this, MinDamage, MaxDamage, skillContext);
        OnSkillEffected?.Invoke();

        skillContext.user.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Deffence)), 4);

        yield return new WaitForSeconds(1.5f);
        OnSkillFinish?.Invoke();
        FocusCamera.Instance.LockingMovingCamera(true);
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
