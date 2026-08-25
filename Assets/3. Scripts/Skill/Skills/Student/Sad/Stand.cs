using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stand : Skill, ITargetedMeSkill
{
    public override IEnumerator Effected(SkillContext skillContext)
    {
        OnSkillAction?.Invoke(ReturnData());

        OnSkillStart?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attackReady));

        yield return new WaitForSeconds(1f);

        if(skillContext.user.iconlist.OfType<Deffence>().Any())
        {
            int a = skillContext.user.iconlist.First(x => x is Deffence).stack / 2;
            skillContext.user.iconlist.First(x => x is Deffence).stack = a;

            skillContext.user.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Power)), a);
        }

        OnSkillEffected?.Invoke();

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
