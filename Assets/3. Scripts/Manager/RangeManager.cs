using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    public static RangeManager instance;

    public List<Character> actingCharacterList = new();

    public Range ourRange;
    public Range enemyRange;

    int nowSelectedNum;
    Character nowSelectedChar;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SchoolManager.instance.OnStarted += SetOurRange;

        FightManager.Instance.OnFighting += SetEnemyRange;
        FightManager.Instance.OnTurnStart += FightTurnSetting;
        FightManager.Instance.OnActingStart += SelectActer;
        FightManager.Instance.OnDyingSomeOne += DiedChar;
        FightManager.Instance.GetRangeData += ReturnRangeData;
    }

    private void OnDisable() // SetActive되는 옵젝이 아니면 이렇게 안해도 되긴 함.
    {
        SchoolManager.instance.OnStarted -= SetOurRange;

        FightManager.Instance.OnTurnStart -= FightTurnSetting;
        FightManager.Instance.OnActingStart -= SelectActer;
    }

    void SetOurRange()
    {
        foreach (Character data in ImportantData.usedStudents)
        {
            Instantiate(data, ourRange.transform);
        }

        ourRange.GetCharacter();
    }

    void SetEnemyRange()
    {
        List<Character> list = FightManager.Instance.GetNowEnemys?.Invoke().enemyList;

        foreach (Character character in list)
        {
            Instantiate(character, enemyRange.transform);
        }

        enemyRange.GetCharacter();
    }

    void FightTurnSetting()
    {
        actingCharacterList.Clear();
        nowSelectedNum = 0;

        actingCharacterList.AddRange(ourRange.GetCharacter());
        actingCharacterList.AddRange(enemyRange.GetCharacter());

        SetActingCharacter();
    }

    CharacterRangeData ReturnRangeData()
    {
        CharacterRangeData rangeData = new CharacterRangeData
        {
            nowSelectedChar = this.nowSelectedChar,
            nowSelectedNum = this.nowSelectedNum,
            allCharacterList = actingCharacterList,
            ourRangeChar = ourRange.GetCharacter(),
            enemyRangeChar = enemyRange.GetCharacter()
        };

        return rangeData;
    }

    void SetActingCharacter() // 로직: 캐릭마다 최소 - 최대 속도 중 랜덤 속도 결정 후, 리스트 섞음. 속도가 작은 순으로 배열 후, 가장 빠른 놈부터 선공권 부여. 
    {
        foreach (Character character in actingCharacterList)
        {
            character.SetSpeed();
        }

        // 순서 배정 전 랜덤 섞기
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int r1 = UnityEngine.Random.Range(0, actingCharacterList.Count);
            int r2 = UnityEngine.Random.Range(0, actingCharacterList.Count);

            Templet.SwapTwoCollectionValue(actingCharacterList, r1, r2);
        }

        // 순서 정하기 ( 배열 정리 )
        actingCharacterList.Sort((a, b) => b.speed.CompareTo(a.speed)); // 이런 간편한 정렬 기능이 있었다고 ????? ( b -> a 는 높은 순 정렬.)

        // 실제 순서 배정 ( 정리된 배열에 값 넣어주기 )
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int giveTurnCount = i + 1;

            actingCharacterList[i].nowTurnCount = giveTurnCount;
        }
    }

    void SelectActer()
    {
        nowSelectedChar = actingCharacterList[nowSelectedNum];
        nowSelectedNum++;

        FocusCamera.Instance.LivingAndTargeting(nowSelectedChar);
    }

    void DiedChar(GameObject dyingChar)
    {
        int targetNum = 0;
        Action act = () =>
        {

        };

        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            if (actingCharacterList[i].gameObject == dyingChar)
            {
                targetNum = i;

                if (actingCharacterList[i].speed == nowSelectedNum)
                {
                    act = () =>
                    {
                        FightManager.Instance.OnActingFinished?.Invoke();
                    };
                }
                break;
            }
        }

        actingCharacterList.RemoveAt(targetNum);
        Destroy(dyingChar);
        act?.Invoke();
    }

    public void RangeClear()
    {
        ourRange.Clear();
        enemyRange.Clear();
    }

    public void CharacterExp()
    {
        ourRange.Exp();
    }
}
