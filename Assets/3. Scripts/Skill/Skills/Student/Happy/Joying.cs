using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[Serializable]
public class Joying : Skill, ITargetedMeSkill
{
    public override IEnumerator Effected(SkillContext skillContext)
    {
        OnSkillAction?.Invoke(ReturnData());

        OnSkillStart?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.buff));
        
        yield return new WaitForSeconds(0.5f);

        int r1 = UnityEngine.Random.Range(3, 6);
        skillContext.user.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Power)), r1);
        skillContext.user.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Speed)), 2);
        skillContext.user.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Broken)), 2);

        foreach (Character character in skillContext.ourRangeChar)
        {
            if (character == skillContext.user)
            {
                continue;
            }

            int r2 = UnityEngine.Random.Range(2, 5);

            character.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Power)), r2);
            character.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Speed)), 2);
            character.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Broken)), 2);
        }

        foreach (Character ene in skillContext.enemyRangeChar)
        {
            int r = UnityEngine.Random.Range(2, 4);

            ene.AddIcon(SkillTemplet.FindIcon(data.skillIcons, typeof(Broken)), r);
        }

        OnSkillEffected?.Invoke();

        yield return new WaitForSeconds(0.5f);
        OnSkillFinish?.Invoke();
        skillContext.user.SetImage(skillContext.user.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));

        FightManager.Instance.OnActingFinished?.Invoke();
        FocusCamera.Instance.Live(0);
    }
}
