using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RangeManager : MonoBehaviour // Range 데이터를 가지는 매니져(였던 것...)
{
    public List<Character> actingCharacterList = new();

    public Range ourRange;
    public Range enemyRange;

    int nowSelectedNum;
    Character nowSelectedChar;

    private void OnEnable()
    {
        FightManager.Instance.OnTurnStart += FightTurnSetting;
        FightManager.Instance.OnActingStart += SelectActer;
        FightManager.Instance.GetRangeData += ReturnRangeData;
    }

    private void OnDisable() // SetActive되는 옵젝이 아니면 이렇게 안해도 되긴 함.
    {
        FightManager.Instance.OnTurnStart -= FightTurnSetting;
        FightManager.Instance.OnActingStart -= SelectActer;
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

    void SetActingCharacter()
    {
        // 순서 배정 전 랜덤 섞기
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int r1 = Random.Range(0, actingCharacterList.Count);
            int r2 = Random.Range(0, actingCharacterList.Count);

            Templet.SwapTwoCollectionValue(actingCharacterList, r1, r2);
        }

        // 순서 정하기 ( 배열 정리 )
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int giveSpeed = i + 1;

            if (actingCharacterList[i].minSpeed < giveSpeed)
            {
                int minSpeed = actingCharacterList[i].minSpeed;
                if (minSpeed >= actingCharacterList.Count)
                {
                    minSpeed = actingCharacterList.Count - 1;
                }

                Templet.SwapTwoCollectionValue(actingCharacterList, i, minSpeed);
            }
        }

        // 실제 순서 배정 ( 정리된 배열에 값 넣어주기 )
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int giveSpeed = i + 1;

            actingCharacterList[i].speed = giveSpeed;
        }
    }

    void SelectActer()
    {
        nowSelectedChar = actingCharacterList[nowSelectedNum];
        nowSelectedNum++;

        GameEvent.OnNoticedSomething($"{nowSelectedChar.characterName}의 차례!");

        School_FocusCamera.Instance.LivingAndTargeting(nowSelectedChar);

        FightManager.Instance.WhatSelcetedActingChar?.Invoke(nowSelectedChar);
        FightManager.Instance.OnActingCharSelceted?.Invoke();
    }
}
