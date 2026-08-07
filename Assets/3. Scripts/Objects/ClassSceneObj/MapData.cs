using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public int mapCount;
    public Action OnReseted;

    List<Room> rooms = new();

    public void Initialize(List<Room> rooms)
    {
        this.rooms = rooms;
    }

    private void Awake()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<MapIcon>().Initialize(i, data.nowFloor, this);
        }
    }
}
