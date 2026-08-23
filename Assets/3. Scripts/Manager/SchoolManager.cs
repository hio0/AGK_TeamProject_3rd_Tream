using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SchoolManager : MonoBehaviour
{
    public static SchoolManager instance;

    public event Action OnStarted;
    public event Action OnFinished;

    public Action OnNextClass;
    public Action OnNextRoom;
    public Action OnNextFloor;
    public event Action<int, int> OnTimerSet;

    public Action OnAgitScene;
    public Action OnElevatorScene;

    public Action<List<KeyValuePair<ItemData, int>>> OnItemFind;
    public Action OnItemFinding;
    public Action<bool> OnNextFind;
    public Action OnItemNext;
    public Action OnItemFindEnd;

    public Action<string> OnNoticedSomething; // 나레이션할만한 행동 일어남
    public Action<int> OnMoneyChanged;

    public Func<int> GetMapIcon;
    public ItemData deduct;

    [Header("시스템")]
    float timer;
    [SerializeField] TMP_Text timerT;
    [SerializeField] float timerPlusCool;
    public bool isTimerSet;

    [SerializeField] RectTransform startP;
    [SerializeField] SpeakData defultData;
    [SerializeField] SpeakBox pre_SpeakBox;

    private void Awake()
    {
        instance = this;

        StartSetting();
    }

    // Start is called before the first frame update
    void Start()
    {
        IEnumerator Cor()
        {
            UIMovement.DoAnchorMove(startP, new Vector2(-2461.2f, 0), 1.5f);

            yield return new WaitForSeconds(1.5f);

            OnNextClass?.Invoke();
            OnNextFloor?.Invoke();
            OnStarted?.Invoke();

            isTimerSet = true;
            timer = 1;
        }
        StartCoroutine(Cor());

        FightManager.Instance.OnFighting += TimerActive;
        FightManager.Instance.OnFightFinish += TimerActive;

        UpdateTimeText();
        StartCoroutine(TimeRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerSet)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
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

    void TimerActive()
    {
        isTimerSet = !isTimerSet;
        timer = timerPlusCool;
    }

    public void Speak(SpeakData data, string speach, Transform parent)
    {
        List<string> list = new();
            
        list.AddRange(GetVariable(data, speach));
        list.AddRange(GetVariable(defultData, speach));

        List<string> GetVariable(SpeakData obj, string variableName)
        {
            FieldInfo field = obj.GetType().GetField(
                variableName,
                BindingFlags.Public | BindingFlags.Instance
            );

            if (field == null)
                return null;

            return (List<string>)field.GetValue(obj);
        }

        string massage = list[UnityEngine.Random.Range(0, list.Count)];
        SpeakBox box = Instantiate(pre_SpeakBox, parent);

        box.Initialize(massage);
    }
}
