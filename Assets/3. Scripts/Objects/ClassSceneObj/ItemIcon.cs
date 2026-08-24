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
    [SerializeField] Transform parent_basicIcon;

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
        basicIcon = Instantiate(pre_basicIcon, parent_basicIcon);
        MoveWithMouse(eventData);

        basicIcon.bgImage.color = new Color32(0, 0, 0, 0);
        basicIcon.strokeImage.color = new Color32(0, 0, 0, 0);
        basicIcon.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIcon.spriteImage.sprite = myItem.itemImage;

        basicIcon.can.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveWithMouse(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(basicIcon.gameObject);
    }

    void MoveWithMouse(PointerEventData eventData)
    {
        RectTransform parentRect = basicIcon.transform.parent as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos
        );

        basicIcon.GetComponent<RectTransform>().anchoredPosition = localPos;
    }

    void NullValue()
    {
        icon.spriteImage.sprite = null;
        icon.spriteImage.color = new Color32(255, 255, 255, 0);
        countT.gameObject.SetActive(false);
    }
}
