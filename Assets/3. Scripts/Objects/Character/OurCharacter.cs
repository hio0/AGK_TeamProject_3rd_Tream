using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

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

        foreach (Character targetchar in targetCharList)
        {
            targetchar.ReturnToBasic();

            Action<PointerEventData> onEnter = (pointEventData) =>
            {
                targetchar.selectingTargets = MultifulTargeting(targetchar, myskill);

                targetchar.OnTriggerEnter?.Invoke();
            };

            Action<PointerEventData> onClick = (pointEventData) =>
            {
                skillContext = MakeSkillContext(myskill, targetchar.selectingTargets);
                FightManager.Instance.OnTargetFinded?.Invoke();


                targetchar.OnTriggerClick?.Invoke();
            };

            Action<PointerEventData> onExit = (pointEventData) =>
            {
                targetchar.iSelecting = false;

                targetchar.OnTriggerExit?.Invoke();
            };

            Templet.AddEvent(targetchar.characterTrigger, EventTriggerType.PointerEnter, onEnter);
            Templet.AddEvent(targetchar.characterTrigger, EventTriggerType.PointerClick, onClick);
            Templet.AddEvent(targetchar.characterTrigger, EventTriggerType.PointerExit, onExit);
        }
    }
}
