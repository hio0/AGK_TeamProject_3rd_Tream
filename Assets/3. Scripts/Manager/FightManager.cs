using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [Header("시스템")]
    public static FightManager Instance;

    public event Action OnFightStart;
    public event Action OnTurnStart;
    public event Action OnCharSelceted;
    public event Action OnTurnFinish;

    public Transform OurRange;
    public Transform EnemyRange;
    public int nowSelecedNum;
    public Character nowSelectedChar;

    [Header("오브젝트")]
    public SkillInfo pre_skillInfo;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FightStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 시스템
    public void FightStart()
    {
        BasicEventSet();
        OnFightStart?.Invoke();

        TurnStart();
    }

    void BasicEventSet()
    {
        Action a = () =>
        {
            nowSelecedNum -= 1;
            SelectActingCharacter();
        };

        Action d = () =>
        {
            nowSelecedNum += 1;
            SelectActingCharacter();
        };

        Action firstSelect = () =>
        {
            InputManager.Instance.OnPressA += a;
            InputManager.Instance.OnPressD += d;

            nowSelecedNum = OurRange.childCount - 1;
            SelectActingCharacter();
        };

        OnFightStart += firstSelect;
    }

    public void TurnStart()
    {
        OnTurnStart?.Invoke();
    }

    void SelectActingCharacter()
    {
        if (nowSelecedNum < 0)
        {
            nowSelecedNum = 0;
            return;
        }
        if (nowSelecedNum > transform.childCount - 1)
        {
            nowSelecedNum = transform.childCount - 1;
            return;
        }

        nowSelectedChar = OurRange.GetChild(nowSelecedNum).GetComponent<Character>();
        OnCharSelceted?.Invoke();
    }

    public void TurnFinish()
    {
        OnTurnFinish?.Invoke();
    }

    public void ClearEvent()
    {
        OnFightStart = null;
        OnTurnStart = null;
        OnTurnFinish = null;
    }

    // 기능 템플릿
    public void SkillInfoSet(RectTransform rect)
    {

    }
}
