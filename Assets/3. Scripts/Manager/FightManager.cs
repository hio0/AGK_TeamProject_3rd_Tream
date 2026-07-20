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

    public Transform ourRange;
    public Transform enemyRange;
    public int nowSelecedNum;
    public Character nowSelectedChar;
 

    private void Awake()
    {
        Instance = this;

        BasicSetting();
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
        OnFightStart?.Invoke();

        TurnStart();
    }

    void BasicSetting()
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
        };
        OnFightStart += firstSelect;
        OnTurnStart += SelectActingCharacter;

        nowSelecedNum = ourRange.childCount - 1;
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
        if (nowSelecedNum > ourRange.childCount - 1)
        {
            nowSelecedNum = ourRange.childCount - 1;
            return;
        }

        nowSelectedChar = ourRange.GetChild(nowSelecedNum).GetComponent<Character>();
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
