using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    Skill myskill;
    SkillExplanation skillExplanation;
    Character user;

    Image image;
    EventTrigger trigger;

    public void Initialize(Skill skill, SkillExplanation skillExplan, Character user)
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

        FightManager.Instance.WhatUserAndSelectedSkill?.Invoke(user, myskill);
        FightManager.Instance.OnTargetFinding?.Invoke();
        GameEvent.OnNoticedSomething("타겟을 정하자!");
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
