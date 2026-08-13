using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapIcon : MonoBehaviour
{
    int roomNum;
    int floorNum;
    MapData p;

    public static int nowNum;
    public static List<GameObject> nowIcons = new();
    public List<GameObject> conectIcons = new();

    public BasicIcon icon;
    public Toggle toggle;
    public EventTrigger trigger;

    public bool elevatiorB;

    public void Initialize(int num, int nowfloor, MapData p)
    {
        roomNum = num;
        floorNum = nowfloor;
        this.p = p;
    }

    int ReturnData()
    {
        return roomNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        Templet.AddEvent(trigger, EventTriggerType.PointerClick, OnClick);

        icon.ReturnImage();

        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();
        if (data.nowRoomNum == roomNum && data.nowFloor == floorNum)
        {
            Select();
        }
    }

    private void OnEnable()
    {
        p.OnReseted += ResetMe;
    }

    private void OnDisable()
    {
        p.OnReseted -= ResetMe;
        SchoolManager.instance.GetMapIcon -= ReturnData;
    }

    void ResetMe()
    {
        if (nowNum != roomNum)
        {
            toggle.isOn = false;
            icon.bgImage.color = new Color32(28, 28, 34, 255);
        }
    }


    public void OnClick(PointerEventData eventData)
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();

        if (data.nowFloor == floorNum)
        {
            if(elevatiorB)
            {
                Select();

                SceneMoveManager.Instance.FadeSceneLoad("Elevator", 1f);
                SchoolManager.instance.OnNextFloor?.Invoke();
            }

            if (nowIcons.Contains(gameObject))
            {
                Select();

                SchoolManager.instance.GetMapIcon -= ReturnData;
                SchoolManager.instance.GetMapIcon += ReturnData;

                SchoolManager.instance.OnNextRoom?.Invoke();
            }

            p.OnReseted?.Invoke();
        }
    }

    void Select()
    {
        icon.ReturnImage();

        toggle.isOn = true;
        icon.bgImage.color = new Color32(148, 148, 195, 255);

        nowNum = roomNum;
        nowIcons = conectIcons;
    }
}
