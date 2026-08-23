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
    protected override void Awake()
    {
        base.Awake();

        SchoolManager.instance.OnItemFinding += Opinion;
        SchoolManager.instance.OnItemNext += Opinion;
        SchoolManager.instance.OnItemFindEnd += OpinionExit;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnItemFinding -= Opinion;
        SchoolManager.instance.OnItemNext -= Opinion;
        SchoolManager.instance.OnItemFindEnd -= OpinionExit;
    }

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
            if (targetchar.iTargeting)
            {
                targetCharList.Add(targetchar);
            }
        }

        foreach (Character target in targetCharList)
        {
            target.ReturnToBasic();
            mychar.characterTrigger.triggers.RemoveAll(entry => entry.eventID == EventTriggerType.PointerEnter || entry.eventID == EventTriggerType.PointerExit || entry.eventID == EventTriggerType.PointerClick);

            void OnEnter(Character targetchar)
            {
                targetchar.selectingTargets = MultifulTargeting(targetchar, myskill);
            }

            void OnClick(Character targetchar)
            {
                skillContext = MakeSkillContext(myskill, targetchar.selectingTargets);

                FightManager.Instance.OnTargetFinded?.Invoke();
                SchoolManager.instance.OnNoticedSomething($"{mychar.characterName}의\n{myskill.data.skillName}!");
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

    void Opinion()
    {
        List<Character> sameCharacterList = new();
        bool randomBool = UnityEngine.Random.value > 0.5f;
        mychar.isapproval = randomBool;

        OpinionExit();

        void OnEnter(PointerEventData eventData)
        {
            sameCharacterList.Clear();
            mychar.characterOutLine.enabled = true;

            CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();
            foreach(Character character in rangeData.ourRangeChar)
            {
                if(character.isapproval== mychar.isapproval)
                {
                    character.characterOutLine.enabled = true;
                    sameCharacterList.Add(character);
                }
            }

            string a = null;
            if(mychar.isapproval)
            {
                a = "opinionSpeach_approval";
                mychar.characterOutLine.effectColor = new Color32(130, 158, 243, 255);
            }
            else
            {
                a = "opinionSpeach_opposite";
                mychar.characterOutLine.effectColor = new Color32(243, 130, 138, 255);
            }
            SchoolManager.instance.Speak(mychar.characterData.speakData, a, this.transform);
        }

        void OnClick(PointerEventData eventData)
        {
            SchoolManager.instance.OnNextFind(mychar.isapproval);
            SchoolManager.instance.OnItemNext();
        }

        void OnExit(PointerEventData eventData)
        {
            mychar.characterOutLine.enabled = false;
            foreach(Character character in sameCharacterList)
            {
                character.characterOutLine.enabled = false;
            }
        }

        Templet.AddEvent(mychar.characterTrigger, EventTriggerType.PointerEnter, OnEnter);
        Templet.AddEvent(mychar.characterTrigger, EventTriggerType.PointerClick, OnClick);
        Templet.AddEvent(mychar.characterTrigger, EventTriggerType.PointerExit, OnExit);
        
    }

    void OpinionExit()
    {
        mychar.characterTrigger.triggers.RemoveAll(entry => entry.eventID == EventTriggerType.PointerEnter || entry.eventID == EventTriggerType.PointerExit || entry.eventID == EventTriggerType.PointerClick);
    }
}
