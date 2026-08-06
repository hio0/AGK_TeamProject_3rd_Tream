using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StudentIcon : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.PointerEnter, OnEnter);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.PointerExit, OnExit);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.Drop, OnDrop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnter(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            if(eventData.pointerDrag.TryGetComponent<StudentInfo>(out StudentInfo info))
            {
                BasicIconData basicIconData = GetComponent<BasicIcon>().ReturnImage();

                basicIconData.strokeImage.color = new Color32(148, 148, 195, 255);
            }
        }
    }

    public void OnExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            if (eventData.pointerDrag.TryGetComponent<StudentInfo>(out StudentInfo info))
            {
                BasicIconData basicIconData = GetComponent<BasicIcon>().ReturnImage();

                basicIconData.strokeImage.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        StudentInfo draggedObject = eventData.pointerDrag.GetComponent<StudentInfo>();
        BasicIcon basicIcon = GetComponent<BasicIcon>();

        BasicIconData basicIconData = basicIcon.ReturnImage();

        basicIconData.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIconData.spriteImage.sprite = draggedObject.icon.sprite;

        ImportantData.usedStudents.Add(draggedObject.mychar);
    }
}
