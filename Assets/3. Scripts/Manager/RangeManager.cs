using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    public List<Character> actingCharacterList = new();

    public Range ourRange;
    public Range enemyRange;

    int nowSelectedNum;
    Character nowSelectedChar;

    private void OnEnable()
    {
        FightManager.Instance.OnTurnStart += FightTurnSetting;
        FightManager.Instance.OnActingFinished += SelectActer;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnTurnStart -= FightTurnSetting;
        FightManager.Instance.OnActingFinished -= SelectActer;
    }

    void FightTurnSetting()
    {
        actingCharacterList.Clear();
        nowSelectedNum = 0;

        actingCharacterList.AddRange(ourRange.GetCharacter());
        actingCharacterList.AddRange(enemyRange.GetCharacter());

        SetActingCharacter();
    }

    void SetActingCharacter()
    {
        void SwapTwoCollectionValue<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        // 순서 배정 전 랜덤 섞기
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int r1 = Random.Range(0, actingCharacterList.Count);
            int r2 = Random.Range(0, actingCharacterList.Count);

            SwapTwoCollectionValue(actingCharacterList, r1, r2);
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

                Debug.Log($"{i}, {minSpeed}");
                SwapTwoCollectionValue(actingCharacterList, i, minSpeed);
            }
        }

        // 실제 순서 배정 ( 정리된 배열에 값 넣어주기 )
        for (int i = 0; i < actingCharacterList.Count; i++)
        {
            int giveSpeed = i + 1;

            actingCharacterList[i].speed = giveSpeed;
        }

        SelectActer();
    }

    void SelectActer()
    {
        nowSelectedChar = actingCharacterList[nowSelectedNum];
        nowSelectedNum++;

        CharacterSelected characterSelected = new CharacterSelected
        {
            selectedNum = nowSelectedNum,
            selectedCharacter = nowSelectedChar
        };

        FightManager.Instance.OnCharSelceted?.Invoke(characterSelected);
    }
}
