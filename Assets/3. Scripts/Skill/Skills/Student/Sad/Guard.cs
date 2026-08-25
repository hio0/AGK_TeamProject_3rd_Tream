using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Guard : Skill, ITargetedMeSkill
{
    public override IEnumerator Effected(SkillContext skillContext)
    {
        OnSkillAction?.Invoke(ReturnData());

        OnSkillStart?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.attackReady));

        yield return new WaitForSeconds(1f);

        skillContext.user.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Deffence)), 2);

        foreach (Character character in skillContext.ourRangeChar)
        {
            if (character == skillContext.user)
            {
                continue;
            }

            int a = skillContext.user.iconlist.First(x => x is Deffence).stack;

            character.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Deffence)), a);
        }

        OnSkillEffected?.Invoke();

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
