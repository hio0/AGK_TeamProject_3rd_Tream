using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StudentIcon : MonoBehaviour
{
    public Character dragObject;
    BasicIcon basicIcon;
    public BasicIcon pre_basicIcon;

    // Start is called before the first frame update
    void Start()
    {
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.PointerEnter, OnEnter);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.PointerExit, OnExit);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.BeginDrag, OnBeginDrag);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.Drag, OnDrag);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.EndDrag, OnEndDrag);
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
            if(eventData.pointerDrag.TryGetComponent<StudentInfo>(out StudentInfo info) && dragObject == null)
            {
                BasicIconData basicIconData = GetComponent<BasicIcon>().ReturnImage();

                basicIconData.bgImage.color = new Color32(28, 28, 34, 255);
                basicIconData.strokeImage.color = new Color32(148, 148, 195, 255);
            }
        }
    }

    public void OnExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            if (eventData.pointerDrag.TryGetComponent<StudentInfo>(out StudentInfo info) && dragObject == null)
            {
                ResetIcon(eventData);
            }
        }
    }

    void ResetIcon(PointerEventData eventData)
    { 
        BasicIconData basicIconData = GetComponent<BasicIcon>().ReturnImage();

        basicIconData.spriteImage.color = new Color32(0, 0, 0, 0);
        basicIconData.bgImage.color = new Color32(28, 28, 34, 255);
        basicIconData.strokeImage.color = new Color32(255, 255, 255, 255);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransform rect = GameManager.instance.GetUIRect?.Invoke();
        basicIcon = Instantiate(pre_basicIcon, rect);
        MoveWithMouse(eventData);

        BasicIconData basicIconData = basicIcon.ReturnImage();
        basicIconData.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIconData.strokeImage.color = new Color32(148, 148, 195, 255);
        basicIconData.spriteImage.sprite = dragObject.characterData.iconImage;
        basicIconData.canvasGroup.blocksRaycasts = false;

        ResetIcon(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveWithMouse(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragObject = null;

        Destroy(basicIcon.gameObject);
        basicIcon = null;

        GameManager.instance.OnDroped?.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Character drag = eventData.pointerDrag.GetComponent<StudentInfo>().mychar;
        SetIcon(drag);
    }

    public void SetIcon(Character drag)
    {
        dragObject = drag;
        BasicIcon basicIcon = GetComponent<BasicIcon>();

        BasicIconData basicIconData = basicIcon.ReturnImage();

        basicIconData.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIconData.spriteImage.sprite = dragObject.characterData.iconImage;

        GameManager.instance.OnDroped?.Invoke();
    }

    void MoveWithMouse(PointerEventData eventData)
    {
        RectTransform rect = GameManager.instance.GetUIRect?.Invoke();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos
        );

        basicIcon.GetComponent<RectTransform>().anchoredPosition = localPos;
    }
}
