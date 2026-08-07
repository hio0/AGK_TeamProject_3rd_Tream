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
    // * 중간에 변경. 본래 객체들끼리 완전 고립, 이벤트로만 값을 주고받고자 하였으나 객체들끼리 데이터를 주고 받지 않고 데이터 열람권을 주고받아 직접 참조하도록 변경. 정해진 데이터만 주고받던 기존에 비해 고립성을 줄인 대신 상호작용 증가.

    public Action OnFighting;
    public event Action OnFightStart;
    public event Action OnTurnStart;
    public event Action OnTurnFinish;
    public event Action OnFightFinish;

    public Action OnActingStart;
    public Action OnActingFinished;

    public Action OnTargetFinding;
    public Action OnTargetFinded;

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
        OnFighting += FightStart;
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
            FocusCamera.Instance.Live(0);
            DownLetterBox_UI.Instance.Move();

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
