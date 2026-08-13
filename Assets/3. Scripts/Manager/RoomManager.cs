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
    public List<Room> hallways = new();
    public List<Room> rooms = new();

    public int nowRoomNum;
    public Room nowRoom;
    public int nowFloor;

    public int floorCount;
    public Dictionary<int, GameObject> floorRoomList = new();
    public List<GameObject> mapDatas;
    public Dictionary<int, List<Room>> roomList = new();

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
            nowRoom = nowRoom
        };

        return data;
    }

    void MakeSchool()
    {
        SetFloorCount();
        SetRoomCount();

        FloorSet();
        RoomSet(0);
    }

    /// <summary>
    /// 평균 전투 개수: 4개 / 평균 방 개수 : 7개
    /// </summary>
    /// 
    // 일차 x 당 방 개수 1 추가 / x는 일차 3개 당 1 증가
    // 3일차부터 층 1 추가 / 5일차에 1추가 / 그 이후로 일차 3개 당 1 증가

    void SetFloorCount()
    {
        int plusFloor = 0;
        plusFloor = ImportantData.dayCount / 4;
        if (ImportantData.dayCount >= 5)
        {
            plusFloor++;
        }

        floorCount = 1 + plusFloor;
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
        floorRoomList.Add(nowFloor, list[ran]);
    }

    void FloorSet()
    {
        nowFloor = ImportantData.nowFloorCount;
        Debug.Log($"floorset: {nowFloor} / {ImportantData.nowFloorCount}");
    }

    void SelectRoom()
    {
        int num = SchoolManager.instance.GetMapIcon.Invoke();

        RoomSet(num);
    }

    void RoomSet(int num)
    {
        nowRoomNum = num;
        Debug.Log($"floorset: {nowFloor}");
        nowRoom = roomList[nowFloor][nowRoomNum];

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        Room room = Instantiate(nowRoom, transform);

        int r = Random.Range(0, 3);
        for(int i = 0; i < r; i++)
        {
            int trsR = Random.Range(0, room.objectTransform.Count);
            int objR = Random.Range(0, room.objects.Count);

            if (room.objectTransform[trsR].childCount != 0)
            {
                i--;
                continue;
            }

            if(trsR != 0 && objR != 0)
            {
                Instantiate(room.objects[objR], room.objectTransform[trsR]);
            }
        }
    }

    void NextFloor()
    {
        ImportantData.maxFloorCount = floorCount;
        ImportantData.nowFloorCount = nowFloor + 1;
        ImportantData.floorRoomsList = floorRoomList;
    }
}
