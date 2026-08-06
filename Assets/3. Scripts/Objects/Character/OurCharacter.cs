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
        Character user = FightManager.Instance.GetRangeData?.Invoke().nowSelectedChar;
        Skill skill = FightManager.Instance.GetNowSkill?.Invoke();

        mychar.iTargeting = skill.CanCharacterTargeting(user, mychar);
    }

    protected override void TargetFinding(Character mainchar)
    {
        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();
        if (rangeData.nowSelectedChar.speed != mainchar.speed)
        {
            return;
        }
        Skill myskill = FightManager.Instance.GetNowSkill?.Invoke();

        foreach(Character targetchar in rangeData.allCharacterList)
        {
            if (targetchar.iTargeting)
            {
                targetchar.ReturnToBasic();

                Action<PointerEventData> onEnter = (pointEventData) =>
                {
                    targetchar.selectingTargets = MultifulTargeting(targetchar, myskill);

                    targetchar.OnTriggerEnter?.Invoke();
                };

                Action<PointerEventData> onClick = (pointEventData) =>
                {
                    SkillContext context = MakeSkillContext(myskill, targetchar.selectingTargets);
                    FightManager.Instance.OnTargetFinded?.Invoke(context);

  
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
}
