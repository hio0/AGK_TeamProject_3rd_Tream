using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using static Unity.Collections.AllocatorManager;

public class OurCharacter : CharacterTeam
{
    string a;

    protected override void Awake()
    {
        base.Awake();

        SchoolManager.instance.OnItemFinding += Opinion;
        SchoolManager.instance.OnItemNext += SetOpinion;
        SchoolManager.instance.OnItemFindEnd += OpinionExit;

        Map.Instance.OnMove += Move;
        Map.Instance.OnStop += Stop;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnItemFinding -= Opinion;
        SchoolManager.instance.OnItemNext -= SetOpinion;
        SchoolManager.instance.OnItemFindEnd -= OpinionExit;

        Map.Instance.OnMove -= Move;
        Map.Instance.OnStop -= Stop;
    }

    protected override void ActingStart()
    {
        SchoolManager.instance.OnNoticedSomething($"{mychar.characterName}의 차례!");
    }

    protected override void CanITargeting()
    {
        Skill skill = FightManager.Instance.GetNowSkill?.Invoke().mySkill;

        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();

        foreach (Character targetchar in rangeData.allCharacterList)
        {
            targetchar.selectingTargets.Clear();
            targetchar.iTargeting = skill.CanCharacterTargeting(mychar, targetchar);
        }
    }

    protected override void TargetFinding()
    {
        SkillData skill = FightManager.Instance.GetNowSkill.Invoke();
        Skill myskill = skill.mySkill;
        myskill.Initialize(skill);

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
            mychar.characterTrigger.triggers.RemoveAll(entry =>
    entry.eventID is EventTriggerType.PointerEnter
                  or EventTriggerType.PointerExit
                  or EventTriggerType.PointerClick
);

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

    void SetOpinion()
    {
        bool randomBool = UnityEngine.Random.value > 0.5f;
        mychar.isapproval = randomBool;

        if (mychar.isapproval)
        {
            a = "opinionSpeach_approval";
            mychar.characterOpinion.color = new Color32(130, 158, 243, 255);
            mychar.characterOpinion.text = "찬성";
        }
        else
        {
            a = "opinionSpeach_opposite";
            mychar.characterOpinion.color = new Color32(243, 130, 138, 255);
            mychar.characterOpinion.text = "반대";
        }
    }

    void Opinion()
    {
        List<Character> sameCharacterList = new();

        SetOpinion();

        void OnEnter(PointerEventData eventData)
        {
            sameCharacterList.Clear();
            mychar.characterOpinion.gameObject.SetActive(true);

            CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();
            foreach(Character character in rangeData.ourRangeChar)
            {
                if(character.isapproval== mychar.isapproval)
                {
                    character.characterOpinion.gameObject.SetActive(true);
                    sameCharacterList.Add(character);
                }
            }

            SchoolManager.instance.Speak(mychar.characterData.speakData, a, this.transform);
        }

        void OnClick(PointerEventData eventData)
        {
            OnExit(eventData);
            SchoolManager.instance.OnNextFind(mychar.isapproval);
            SchoolManager.instance.OnItemNext();
        }

        void OnExit(PointerEventData eventData)
        {
            mychar.characterOpinion.gameObject.SetActive(false);
            foreach (Character character in sameCharacterList)
            {
                character.characterOpinion.gameObject.SetActive(false);
            }
        }

        Templet.AddEvent(mychar.characterTrigger, EventTriggerType.PointerEnter, OnEnter);
        Templet.AddEvent(mychar.characterTrigger, EventTriggerType.PointerClick, OnClick);
        Templet.AddEvent(mychar.characterTrigger, EventTriggerType.PointerExit, OnExit);
        
    }

    void OpinionExit()
    {


        mychar.characterTrigger.triggers.RemoveAll(entry =>
    entry.eventID is EventTriggerType.PointerEnter
                  or EventTriggerType.PointerExit
                  or EventTriggerType.PointerClick
);
    }

    void Move()
    {
        mychar.SetImage(mychar.characterData.motionData.Find(x => x.type == MotionData.MotionType.run));
    }

    void Stop()
    {
        mychar.SetImage(mychar.characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));
    }
}
