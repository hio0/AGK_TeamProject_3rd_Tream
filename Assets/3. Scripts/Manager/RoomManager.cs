using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RoomData
{
    public List<Room> hallways = new();
    public List<Room> rooms = new();

    public int nowRoomNum;
    public Room nowRoom;
    public int nowFloor;

    public int floorCount;
    public Dictionary<int, GameObject> floorRoomList = new();
    public Dictionary<int, List<Room>> roomList = new();
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public List<Room> hallways = new();
    public List<Room> rooms = new();

    public int nowRoomNum;
    public Room nowRoom;
    public int nowFloor;

    public int floorCount;
    public Dictionary<int, GameObject> floorRoomList = new();
    public List<GameObject> mapDatas;
    public Dictionary<int, List<Room>> roomList = new();

    public event Action<int> OnNodeSetting;
    public event Action<int> OnNodePass;

    public ItemBox pre_itemBox;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        SchoolManager.instance.OnStarted += SetRoom;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnStarted -= SetRoom;
    }

    void SetRoom()
    {
        int nodeCount = 3 + ImportantData.dayCount / 3;
        if (ImportantData.dayCount >= 5)
        {
            nodeCount++;
        }
        if (ImportantData.nowFloorCount == ImportantData.maxFloorCount)
        {
            nodeCount++;
        }

        OnNodeSetting?.Invoke(nodeCount);
    }

    /*
    // Start is called before the first frame update
    void OnEnable()
    {
        SchoolManager.instance.GetRoomData += ReturnData;

        SchoolManager.instance.OnStarted += MakeSchool;
        SchoolManager.instance.OnNextRoom += SelectRoom;
        SchoolManager.instance.OnNextFloor += NextFloor;
    }

    private void OnDisable()
    {
        SchoolManager.instance.GetRoomData -= ReturnData;

        SchoolManager.instance.OnStarted -= MakeSchool;
        SchoolManager.instance.OnNextRoom -= SelectRoom;
        SchoolManager.instance.OnNextFloor -= NextFloor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    RoomData ReturnData()
    {
        RoomData data = new RoomData
        {
            nowFloor = nowFloor,
            floorCount = floorCount,
            floorRoomList = floorRoomList,
            roomList = roomList,
            nowRoom = nowRoom,
            nowRoomNum = nowRoomNum
        };

        return data;
    }

    void MakeSchool()
    {
        SetFloorCount();
        FloorSet();

        SetRoomCount();
        RoomSet(0);
    }

    /// <summary>
    /// 평균 전투 개수: 4개 / 평균 방 개수 : 7개
    /// </summary>
    /// 
    // 일차 x 당 방 개수 1 추가 / x는 일차 3개 당 1 증가
    // 층 개수: 1개 기본 / 3일차에 1층 추가 / 5일차에 1층 추가 / 그 이후로 일차 4개 당 1 증가

    void SetFloorCount()
    {
        int plusFloor = 0;
        plusFloor = ImportantData.dayCount / 4;
        if (ImportantData.dayCount >= 3)
        {
            plusFloor++;
        }
        if (ImportantData.dayCount >= 5)
        {
            plusFloor++;
        }

        floorCount = 2 + plusFloor;
        ImportantData.maxFloorCount = floorCount;
    }

    /// <summary>
    /// 복도가 나올 확률 : 65% / 갈림길로( 정확히는 꺾인 거지만 ) 나올 확률 : 30%
    /// </summary>
    /// 

    void SetRoomCount()
    {
        int roomCount = 5 + ImportantData.dayCount / 3;
        if (ImportantData.dayCount >= 4)
        {
            roomCount += 1;
        }
        if (roomCount >= 30)
        {
            roomCount = 30;
        }

        roomCount = 7;

        for (int i = 0; i < floorCount; i++)
        {
            List<Room> roooomlist = new();
            for (int j = 0; j < roomCount; j++)
            {
                int r = Random.Range(1, 101);
                Room selectRoom = null;

                if (r <= 65)
                {
                    int randomHallway = Random.Range(0, hallways.Count);
                    selectRoom = hallways[randomHallway];
                }
                else
                {
                    int randomRoom = Random.Range(0, rooms.Count);
                    selectRoom = rooms[randomRoom];
                }

                roooomlist.Add(selectRoom);
            }

            roomList.Add(i + 1, roooomlist);
        }

        for (int i = 0; i < floorCount; i++)
        {
            List<GameObject> list = new();
            foreach (GameObject go in mapDatas)
            {
                MapData mapData = go.GetComponent<MapData>();

                if (mapData.mapCount == roomCount)
                {
                    list.Add(go);
                }
            }

            int ran = Random.Range(0, list.Count);
            floorRoomList.Add(i + 1, list[ran]);
        }
    }

    void FloorSet()
    {
        nowFloor = ImportantData.nowFloorCount;
        if(nowFloor < 0)
        {
            nowFloor = 1;
        }
    }

    void SelectRoom()
    {
        int num = SchoolManager.instance.GetMapIcon.Invoke();

        RoomSet(num);
    }

    void RoomSet(int num)
    {
        nowRoomNum = num;
        nowRoom = roomList[nowFloor][nowRoomNum];

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        Room room = Instantiate(nowRoom, transform);

        if(!room.ioneFoot)
        {
            int r = Random.Range(2, 5);
            for (int i = 0; i < r; i++)
            {
                int trsR = Random.Range(0, room.objectTransform.Count);
                Transform rect = room.objectTransform[trsR].transform;

                if (rect.childCount != 0)
                {
                    return;
                }

                ItemBox box = Instantiate(pre_itemBox, rect);
                box.Initialize(room.objectTransform[trsR].size, room.objectTransform[trsR].color, room.items);
            }
        }

        room.ioneFoot = true;
    }

    void NextFloor()
    {
        ImportantData.nowFloorCount = nowFloor;
        ImportantData.floorRoomsList = floorRoomList;
    }
    */
}
