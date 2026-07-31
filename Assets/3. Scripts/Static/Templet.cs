using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Templet
{
    public static void AddEvent(EventTrigger trigger, EventTriggerType type, Action<PointerEventData> action)
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
}
