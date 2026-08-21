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
        SchoolManager.instance.OnNoticedSomething($"{mychar.characterName}의 차례!");
    }

    protected override void CanITargeting()
    {
        Skill skill = FightManager.Instance.GetNowSkill?.Invoke();

        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();

        foreach (Character targetchar in rangeData.allCharacterList)
        {
            targetchar.selectingTargets.Clear();
            targetchar.iTargeting = skill.CanCharacterTargeting(mychar, targetchar);
        }
    }

    protected override void TargetFinding()
    {
        Skill myskill = FightManager.Instance.GetNowSkill?.Invoke();
        List<Character> targetCharList = new();
        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();
        foreach (Character targetchar in rangeData.allCharacterList)
        {
            if(targetchar.iTargeting)
            {
                targetCharList.Add(targetchar);
            }
        }

        foreach (Character target in targetCharList)
        {
            target.ReturnToBasic();
            target.characterTrigger.triggers.Clear();

            void OnEnter(Character targetchar)
            {
                targetchar.selectingTargets = MultifulTargeting(targetchar, myskill);
            }

            void OnClick(Character targetchar)
            {
                skillContext = MakeSkillContext(myskill, targetchar.selectingTargets);

                FightManager.Instance.OnTargetFinded?.Invoke();
                SchoolManager.instance.OnNoticedSomething($"{mychar.characterName}의 {myskill.skillName}!");
                targetchar.OnTriggerClick?.Invoke();
                
                target.characterTrigger.triggers.Clear();
            }

            void OnExit(Character targetchar)
            {
                targetchar.iSelecting = false;

                targetchar.OnTriggerExit?.Invoke();
            }

            Templet.AddEvent(target.characterTrigger, EventTriggerType.PointerEnter, _ => OnEnter(target)); // 매.변. 사용 안할거다 ㅇㅇ
            Templet.AddEvent(target.characterTrigger, EventTriggerType.PointerClick, _ => OnClick(target));
            Templet.AddEvent(target.characterTrigger, EventTriggerType.PointerExit, _ => OnExit(target));
        }
    }

    protected override void Dying()
    {
        
    }
}
