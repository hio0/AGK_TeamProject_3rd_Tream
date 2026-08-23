using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] BasicIcon icon;
    [SerializeField] TMP_Text countT;
    BasicIcon basicIcon;
    [SerializeField] BasicIcon pre_basicIcon;
    Item dragObject;

    public ItemData myItem { get; private set; }
    public int count { get; private set; }

    public void Initialize(int count, ItemData item)
    {
        myItem = item;
        this.count = count;

        if (myItem == null)
        {
            NullValue();
        }
        else
        {
            countT.gameObject.SetActive(true);

            icon.spriteImage.sprite = myItem.itemImage;
            countT.text = count.ToString();
            icon.spriteImage.color = new Color32(255, 255, 255, 255);
        }

        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.BeginDrag, OnBeginDrag);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.Drag, OnDrag);
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.EndDrag, OnEndDrag);
    }

    // Start is called before the first frame update
    void Awake()
    {
        NullValue();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragObject = myItem.myItem;

        RectTransform rect = GameManager.instance.GetUIRect?.Invoke();
        basicIcon = Instantiate(pre_basicIcon, rect);
        MoveWithMouse(eventData);

        BasicIconData basicIconData = basicIcon.ReturnImage();

        basicIconData.bgImage.color = new Color32(0, 0, 0, 0);
        basicIconData.strokeImage.color = new Color32(0, 0, 0, 0);
        basicIconData.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIconData.spriteImage.sprite = myItem.itemImage;

        basicIconData.canvasGroup.blocksRaycasts = false;
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

    void NullValue()
    {
        icon.spriteImage.sprite = null;
        countT.gameObject.SetActive(false);
    }
}
