using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    SkillData myskill;
    SkillExplanation skillExplanation;

    Image image;
    EventTrigger trigger;

    public void Initialize(SkillData skill, SkillExplanation skillExplan)
    {
        myskill = skill;
        skillExplanation = skillExplan;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        trigger = GetComponent<EventTrigger>();

        SetIcon();

        AddEvent(trigger, EventTriggerType.PointerClick, OnClick);
        AddEvent(trigger, EventTriggerType.PointerEnter, OnEnter);
        AddEvent(trigger, EventTriggerType.PointerExit, OnExit);
    }

    public void SetIcon()
    {
        image.sprite = myskill.skillIcon;
    }

    void AddEvent(EventTrigger trigger, EventTriggerType type, Action<PointerEventData> action)
    {
        if (trigger.triggers == null)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;

        entry.callback.AddListener((data) =>
        {
            action((PointerEventData)data);
        });

        trigger.triggers.Add(entry);
    }

    void SkillExplanation()
    {
        skillExplanation.Initialize(myskill);
        skillExplanation.gameObject.SetActive(true);
    }

    void OnClick(PointerEventData data)
    {
        SkillExplanation();
        FightManager.Instance.OnTargetFinded?.Invoke(true);
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
