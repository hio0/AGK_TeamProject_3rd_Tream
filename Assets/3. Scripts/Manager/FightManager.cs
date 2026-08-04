using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FindTarget
{
    public Skill selectedSkill;
}

public class SkillContext
{
    public Character user;
    public Skill useSkill;
    public List<Character> targets;
    public List<Character> ourRangeChar;
    public List<Character> enemyRangeChar;
}

public class CharacterRangeData
{
    public Character nowSelectedChar;
    public int nowSelectedNum;

    public List<Character> allCharacterList;
    public List<Character> ourRangeChar;
    public List<Character> enemyRangeChar;
}

public class FightManager : MonoBehaviour
{
    [Header("시스템")]
    public static FightManager Instance; // 정적 클래스로 만들어도 되지만, 메모리 누수 때문에...
    // 이벤트 버스: 이벤트 선언자에 대한 의존성만 강화, 이외 객체들간 의존성은 대폭 감소. 외부 값 변경이 아닌 이벤트만으로 정보를 공유하기에 캡슐화에 용이. ( 난 캡슐화하기 위해 이벤트 버스 사용. 캡슐화되는 방법이면 이벤트 사용 X )

    public event Action OnFightStart;
    public event Action OnTurnStart;

    public Action OnActingCharSelceted;
    public Action<Character> WhatSelcetedActingChar; // 값 전달용 액션
    public Action<Character> SetSkillIcon;
    public Action OnTargetFinding;
    public Action<Character, Skill> WhatUserAndSelectedSkill;
    public Func<Character, List<Character>, List<Character>> WhatTargetEntering;
    public Action<SkillContext> OnTargetFinded;
    public event Action OnTurnFinish;

    public Action OnActingStart;
    public Action OnActingFinished;

    public Func<CharacterRangeData> GetRangeData;
    public Func<Skill> GetNowSkill;

    [Header("UI")]
    public GameObject fightP;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // 시스템
    public void FightStart()
    {
        IEnumerator FightStartCor()
        {
            fightP.SetActive(false);
            School_FocusCamera.Instance.Live(0);

            yield return new WaitForSeconds(1.5f); // 전투 시작 연출

            OnFightStart?.Invoke();

            yield return new WaitForSeconds(2f); // 전투 시작 효과들
            TurnStart();
        }

        StartCoroutine(FightStartCor());
    }


    public void TurnStart()
    {
        fightP.SetActive(true);

        OnTurnStart?.Invoke();
        OnActingStart?.Invoke();
    }

    public void ActingFinish()
    {
        IEnumerator ActFinish()
        {
            yield return new WaitForSeconds(1f);

            OnActingStart?.Invoke();
        }

        StartCoroutine(ActFinish());
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
