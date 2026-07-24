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
    public List<Character> actingCharacterList = new();
    public Stack<Character> actingTurnList = new();
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
        //OnTurnStart += SelectActingCharacter;

        nowSelecedNum = ourRange.childCount - 1;
    }

    public void TurnStart()
    {
        OnTurnStart?.Invoke();
    }

    void SelectActingCharacter()
    {
        actingCharacterList.Clear();

        void SetActingCharacterList(Transform range)
        {
            for (int i = 0; i < range.childCount; i++)
            {
                Character character = range.GetChild(i).GetComponent<Character>();

                actingCharacterList.Add(character);
            }
        }

        // 모든 캐릭터 가져오기
        SetActingCharacterList(ourRange);
        SetActingCharacterList(enemyRange);

        void SwapTwoCollectionValue<T>(T targetVal, T changedVal)
        {
            T t = targetVal;
            targetVal = changedVal;
            changedVal = t;
        }

        // 순서 배정 전 랜덤 섞기
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int r = UnityEngine.Random.Range(0, actingCharacterList.Count);

            SwapTwoCollectionValue(actingCharacterList[i], actingCharacterList[r]);
        }

        // 순서 정하기
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int giveSpeed = i + 1;

            if (actingCharacterList[i].minSpeed > giveSpeed)
            {
                int minSpeed = actingCharacterList[i].minSpeed - 1;
                if (actingCharacterList[i].minSpeed > actingCharacterList.Count)
                {
                    minSpeed = actingCharacterList.Count;
                } 

                SwapTwoCollectionValue(actingCharacterList[i], actingCharacterList[minSpeed]);
            }
        }

        // 실제 순서 배정
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int giveSpeed = i + 1;

            actingCharacterList[i].speed = giveSpeed;
        }
    }

    void CharacterActing(Character acter)
    {

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
}
