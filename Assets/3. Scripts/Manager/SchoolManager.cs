using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    public static SchoolManager instance;

    public event Action OnStarted;
    public event Action OnFinished;

    public event Action OnNextClass;
    public  Action OnNextRoom;
    public Action OnNextFloor;

    public Action<string> OnNoticedSomething; // 나레이션할만한 행동 일어남

    public Func<int> GetMapIcon;
    public Func<RoomData> GetRoomData;

    private void Awake()
    {
        instance = this;

        StartSetting();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnStarted?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartSetting()
    {
        ImportantData.SetDefultValue();

        ImportantData.dayCount = 1;
        ImportantData.nowFloorCount = 1;
    }
}
