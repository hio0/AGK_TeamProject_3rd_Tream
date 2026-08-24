using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StudentIcon : MonoBehaviour
{
    [SerializeField] BasicIcon basicIcon;

    // Start is called before the first frame update
    void Start()
    {
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.Drop, OnDrop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Character drag = eventData.pointerDrag.GetComponentInParent<StudentInfo>().mychar;
        SetIcon(drag);
    }

    public void SetIcon(Character drag)
    {
        basicIcon.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIcon.spriteImage.sprite = drag.characterData.iconImage;

        ImportantData.usedStudents.Add(drag);
    }
}
