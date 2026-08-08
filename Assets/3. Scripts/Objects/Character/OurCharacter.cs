using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Collections.AllocatorManager;

public class OurCharacter : CharacterTeam
{
    protected override void ActingStart()
    {
        FocusCamera.Instance.LockingMovingCamera(true);
    }

    protected override void CanITargeting()
    {
        targetCharList.Clear();
        Skill skill = FightManager.Instance.GetNowSkill?.Invoke();

        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();

        foreach (Character targetchar in rangeData.allCharacterList)
        {
            targetchar.selectingTargets.Clear();
            targetchar.iTargeting = skill.CanCharacterTargeting(mychar, targetchar);

            if(targetchar.iTargeting)
            {
                targetCharList.Add(targetchar);
            }
        }
    }

    protected override void TargetFinding()
    {
        Skill myskill = FightManager.Instance.GetNowSkill?.Invoke();

        foreach (Character target in targetCharList)
        {
            target.ReturnToBasic();

            void OnEnter(Character targetchar)
            {
                targetchar.selectingTargets = MultifulTargeting(targetchar, myskill);
            }

            void OnClick(Character targetchar)
            {
                skillContext = MakeSkillContext(myskill, targetchar.selectingTargets);

                FightManager.Instance.OnTargetFinded?.Invoke();
                targetchar.OnTriggerClick?.Invoke();
            }

            void OnExit(Character targetchar)
            {
                targetchar.iSelecting = false;

                targetchar.OnTriggerExit?.Invoke();
            }

            target.characterTrigger.triggers.Clear();

            Templet.AddEvent(target.characterTrigger, EventTriggerType.PointerEnter, _ => OnEnter(target)); // 매.변. 사용 안할거다 ㅇㅇ
            Templet.AddEvent(target.characterTrigger, EventTriggerType.PointerClick, _ => OnClick(target));
            Templet.AddEvent(target.characterTrigger, EventTriggerType.PointerExit, _ => OnExit(target));
        }
    }
}
