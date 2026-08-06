using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData
{
    public List<Room> hallways = new();
    public List<Room> rooms = new();

    public int nowRoomNum;
    public Room nowRoom;
    public int nowFloor;

    public int floorCount;
    public Dictionary<int, List<GameObject>> floorRoomList = new();
    public List<Room> roomList = new();
}

public class RoomManager : MonoBehaviour
{
    public List<Room> hallways = new();
    public List<Room> rooms = new();

    public int nowRoomNum;
    public Room nowRoom;
    public int nowFloor;

    public int floorCount;
    public Dictionary<int, List<GameObject>> floorRoomList = new();
    public List<Room> roomList = new();

    private void OnEnable()
    {
        SchoolManager.instance.GetRoomData += ReturnData;

        SchoolManager.instance.OnStarted += MakeSchool;
        SchoolManager.instance.OnNextFloor += SelectFloor;
    }

    // Start is called before the first frame update
    void Start()
    {
        nowFloor = 1;
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
            floorRoomList = floorRoomList
        };

        return data;
    }

    void MakeSchool()
    {
        SetFloorCount();
        SetRoomCount();

        ChangeNowRoom(nowRoomNum + 1);
    }

    /// <summary>
    /// 평균 전투 개수: 4개 / 평균 방 개수 : 7개
    /// </summary>
    /// 
    // 일차 x 당 방 개수 1 추가 / x는 일차 4개 당 1 증가
    // 3일차부터 층 1 추가 / 5일차에 1추가 / 그 이후로 일차 3개 당 1 증가

    void SetFloorCount()
    {
        int plusFloor = 0;
        if (ImportantData.dayCount >= 5)
        {
            plusFloor++;
        }
        plusFloor = ImportantData.dayCount / 3;
        
        floorCount = 1 + plusFloor;
    }

    /// <summary>
    /// 복도가 나올 확률 : 65% / 갈림길로( 정확히는 꺾인 거지만 ) 나올 확률 : 30%
    /// </summary>
    /// 

    void SetRoomCount()
    {
        int roomCount = 3 + Random.Range(2, 5) + ImportantData.dayCount / 4;
        if (roomCount >= 30)
        {
            roomCount = 30;
        }
        Debug.Log(roomCount);

        for (int i = 0; i < roomCount; i++)
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

            roomList.Add(selectRoom);
        }
    }

    void SelectFloor()
    {
        if (floorRoomList.Count <= 0)
        {

        }

        floorCount++;
    }

    void ChangeNowRoom(int change)
    {
        nowRoomNum += change;

        SelectRoom();
    }

    void SelectRoom()
    {
        nowRoom = roomList[nowRoomNum];

        bool isIt = false;
        List<GameObject> rooms = new();
        floorRoomList.Remove(nowFloor);

        for (int i = 0; i < transform.childCount; i++)
        {
            rooms.Add(transform.GetChild(i).gameObject);

            if(i == nowRoomNum)
            {
                isIt = true;
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }

        floorRoomList.Add(nowFloor, rooms);

        if(!isIt)
        {
            Room room = Instantiate(nowRoom, transform);

            int r = Random.Range(1, 101);
            int multLine = 0;

            if(r <= 30)
            {
                multLine = Random.Range(1, 3);
            }

            room.Initialize(multLine);
        }
    }
}
