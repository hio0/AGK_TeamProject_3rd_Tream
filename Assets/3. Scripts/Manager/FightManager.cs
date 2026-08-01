using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelected
{
    public Character selectedCharacter;
}

public class FindTarget
{
    public Skill selectedSkill;
}

public class SkillContext
{
    public Character user;
    public Skill useSkill;
    public List<Character> targets;
}

public class FightManager : MonoBehaviour
{
    [Header("시스템")]
    public static FightManager Instance; // 정적 클래스로 만들어도 되지만, 메모리 누수 때문에...

    // 이벤트 버스: 이벤트 선언자에 대한 의존성만 강화, 이외 객체들간 의존성은 대폭 감소. 외부 값 변경이 아닌 이벤트만으로 정보를 공유하기에 캡슐화에 용이. ( 난 캡슐화하기 위해 이벤트 버스 사용. 캡슐화되는 방법이면 이벤트 사용 X )
    // NOTICE
    // 전투 순서: 전투 시작 -> 턴 시작 -> 캐릭터 선택 -> 행동 시작 -> 스킬 시작 -> 스킬 종료 -> 행동 종료 -> (반복) -> 턴 종료 -> (반복) - > 전투 종료
    public event Action OnFightStart;
    public event Action OnTurnStart;
    public Action OnActingCharSelceted;
    public Action<CharacterSelected> WhatSelcetedActingChar; // 값 전달용 액션
    public Action OnTargetFinding;
    public Action<Character, Skill> WhatUserAndSelectedSkill;
    public Action OnTargetEntering;
    public Action<Character> WhatTargetEntering;
    public Action OnTargetExiting;
    public Action OnTargetClicked;
    public Func<SkillContext> OnTargetFinded;
    public event Action OnTurnFinish;

    public event Action OnActingStart;
    public event Action OnSkillFinished;
    public event Action OnActingFinished;

    [Header("UI")]
    public GameObject fightP;

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
        IEnumerator FightStartCor()
        {
            fightP.SetActive(false);

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
