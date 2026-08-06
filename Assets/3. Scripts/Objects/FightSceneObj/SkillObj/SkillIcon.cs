using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;

public class SkillIcon : MonoBehaviour
{
    Skill myskill;
    SkillExplanation skillExplanation;
    Character user;

    Image image;
    EventTrigger trigger;

    public void Initialize(Skill skill, SkillExplanation skillExplan, Character user) // 사실 이렇게 하기 보단 이벤트로 값 넘겨 받는게 맞긴 하다 ㅇㅇ,,,
    {
        myskill = skill;
        skillExplanation = skillExplan;
        this.user = user;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        trigger = GetComponent<EventTrigger>();

        SetIcon();

        Templet.AddEvent(trigger, EventTriggerType.PointerClick, OnClick);
        Templet.AddEvent(trigger, EventTriggerType.PointerEnter, OnEnter);
        Templet.AddEvent(trigger, EventTriggerType.PointerExit, OnExit);
    }

    void OnEnable()
    {
        FightManager.Instance.OnActingStart += DeleteEvent;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnActingStart -= DeleteEvent;
    }

    Skill ReturnSkill()
    {
        return myskill;
    }

    void DeleteEvent()
    {
        FightManager.Instance.GetNowSkill -= ReturnSkill;
    }

    public void SetIcon()
    {
        image.sprite = myskill.skillIcon;
    }


    void SkillExplanation()
    {
        skillExplanation.Initialize(myskill);
        skillExplanation.gameObject.SetActive(true);
    }

    void OnClick(PointerEventData data)
    {
        SkillExplanation();

        FightManager.Instance.GetNowSkill -= ReturnSkill;
        FightManager.Instance.GetNowSkill += ReturnSkill;

        FightManager.Instance.OnTargetFinding?.Invoke();

        SchoolManager.instance.OnNoticedSomething("타겟을 정하자!");
        FocusCamera.Instance.Live(0);
    }

    void OnEnter(PointerEventData data)
    {
        SkillExplanation();
    }

    void OnExit(PointerEventData data)
    {
        skillExplanation.gameObject.SetActive(false);
    }
}
