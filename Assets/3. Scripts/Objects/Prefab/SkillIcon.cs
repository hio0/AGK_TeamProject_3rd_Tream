using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillIcon : MonoBehaviour
{
    EventTrigger trigger;
    public SkillInfo pre_skillInfo;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<EventTrigger>();

        AddEvent(trigger, EventTriggerType.PointerClick, OnClick);
        AddEvent(trigger, EventTriggerType.PointerEnter, OnEnter);
        AddEvent(trigger, EventTriggerType.PointerExit, OnExit);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void OnClick(PointerEventData data)
    {
        
    }

    void OnEnter(PointerEventData data)
    {
        
    }

    void OnExit(PointerEventData data)
    {
        
    }
}
