using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    public static SchoolManager instance;

    public event Action OnStarted;
    public event Action OnFinished;

    public event Action OnNextClass;
    public Action OnNextRoom;
    public Action OnNextFloor;
    public event Action<int, int> OnTimerSet;

    public Action<string> OnNoticedSomething; // 나레이션할만한 행동 일어남

    public Func<int> GetMapIcon;
    public Func<RoomData> GetRoomData;
    [SerializeField] ItemData deduct;

    [Header("시스템")]
    float timer;
    [SerializeField] TMP_Text timerT;
    [SerializeField] float timerPlusCool;
    public bool isTimerSet;

    private void Awake()
    {
        instance = this;

        StartSetting();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnStarted?.Invoke();
        isTimerSet = true;
        timer = 1;

        FightManager.Instance.OnFighting += TimerActive;
        FightManager.Instance.OnFightFinish += TimerActive;

        ItemManager.Instance.OnAddItem?.Invoke(deduct, 3);

        UpdateTimeText();
        StartCoroutine(TimeRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerSet)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                TimerBlank();
            }
        }
        else
        {
            timerT.gameObject.SetActive(true);
            timer = 1;
        }
    }

    void StartSetting()
    {
        ImportantData.SetDefultValue();

        ImportantData.dayCount = 1;
        ImportantData.nowFloorCount = 1;
    }

    private IEnumerator TimeRoutine()
    {
        while (isTimerSet)
        {
            yield return new WaitForSeconds(timerPlusCool);

            ImportantData.gameMinutes += 5;

            UpdateTimeText();
        }
    }

    private void UpdateTimeText()
    {
        int hour = ImportantData.gameMinutes / 60;
        int minute = ImportantData.gameMinutes % 60;

        timerT.text = $"{hour:00} : {minute:00}";
    }

    void TimerBlank()
    {
        bool blank = timerT.gameObject.activeSelf;
        blank = !blank;
        timerT.gameObject.SetActive(blank);

        timer = 1;
    }

    void TimerActive()
    {
        isTimerSet = !isTimerSet;
        timer = timerPlusCool;
    }
}
