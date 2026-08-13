using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RangeManager : MonoBehaviour
{
    public List<Character> actingCharacterList = new();

    public Range ourRange;
    public Range enemyRange;

    int nowSelectedNum;
    Character nowSelectedChar;

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
    }

    void SetEnemyRange()
    {
        List<EnemyWave> list = SchoolManager.instance.GetRoomData?.Invoke().nowRoom.enemyWaves;

        while (true)
        {
            int r1 = UnityEngine.Random.Range(0, list.Count);
            EnemyWave enemys = list[r1];

            int r2 = UnityEngine.Random.Range(1, 101);
            if (r2 <= enemys.wavePersent)
            {
                foreach (Character character in enemys.enemyList)
                {
                    Instantiate(character, enemyRange.transform);
                }

                break;
            }
        }
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
            int r1 = UnityEngine.Random.Range(0, actingCharacterList.Count);
            int r2 = UnityEngine.Random.Range(0, actingCharacterList.Count);

            Templet.SwapTwoCollectionValue(actingCharacterList, r1, r2);
        }

        // 순서 정하기 ( 배열 정리 )
        for (int i = 0; i < actingCharacterList.Count; i++) // 속도값 << - 이거를 랜덤성
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

                if(actingCharacterList[i].speed == nowSelectedNum)
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
}
