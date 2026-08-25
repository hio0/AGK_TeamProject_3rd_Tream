using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Focus : Skill, ITargetedMeSkill
{
    public override IEnumerator Effected(SkillContext skillContext)
    {
        OnSkillAction?.Invoke(ReturnData());

        OnSkillStart?.Invoke();

        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.buff));
        skillContext.user.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(JipJung)), 3);

        OnSkillEffected?.Invoke();

        yield return new WaitForSeconds(1.5f);

        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
