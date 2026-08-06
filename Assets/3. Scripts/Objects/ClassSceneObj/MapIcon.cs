using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapIcon : MonoBehaviour
{
    public Room myRoom;

    public void Initialize(Room myroom)
    {
        myRoom = myroom;
    }

    // Start is called before the first frame update
    void Start()
    {
        Templet.AddEvent(GetComponent<EventTrigger>(), EventTriggerType.PointerClick, OnClick);
    }

    void OnClick(PointerEventData eventData)
    {
        SchoolManager.instance.OnNextRoom?.Invoke(myRoom);
    }
}
