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

    public Action OnStarted;
    public event Action OnFinished;

    public Action OnNextClass;
    public Action OnNextRoom;
    public Action OnNextFloor;
    public event Action<int, int> OnTimerSet;
    public Action<string> OnRoomChanged;

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

    public List<Character> defultCharacterList;

    [Header("시스템")]
    [SerializeField] RectTransform startP;
    [SerializeField] MiddleManager middleP;

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
        UIMovement.DoAnchorMove(startP, new Vector2(-2461.2f, 0), 1.5f);

        Action ele = () => { MiddleSet(true); GetUse(FightManager.Instance.GetRangeData.Invoke().ourRangeChar); };
        Action agi = () => { MiddleSet(false); GetUse(FightManager.Instance.GetRangeData.Invoke().ourRangeChar); };
        OnElevatorScene += ele;
        OnAgitScene += agi;

        Action st = () => OnRoomChanged("복도");
        Action ag = () => OnRoomChanged("엘리베이터");
        Action lel = () => OnRoomChanged("아지트");
        OnNextFloor += st;
        OnElevatorScene += ag;
        OnAgitScene += lel;

        MiddleSet(false);
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
        ImportantData.canUsedStudents.AddRange(defultCharacterList);
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

    public void GetUse(List<Character> characters)
    {
        ImportantData.canUsedStudents = characters;
    }

    void MiddleSet(bool isEle)
    {
        middleP.gameObject.SetActive(true);
        middleP.Initialize(isEle);
    }

    public void NextDay()
    {
        ImportantData.dayCount++;
    }

    public void SetStart()
    {
        IEnumerator Cor()
        {
            SceneChange.instance.Move(true);

            yield return new WaitForSeconds(1.5f);

            middleP.gameObject.SetActive(false);

            OnStarted?.Invoke();
            OnNextClass?.Invoke();
            OnNextFloor?.Invoke();
        }

        StartCoroutine(Cor());
    }
}
