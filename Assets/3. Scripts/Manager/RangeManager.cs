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

    Character user;
    Skill useSkill;
    List<Character> targets;

    private void OnEnable()
    {
        FightManager.Instance.OnTurnStart += FightTurnSetting;
        FightManager.Instance.OnActingFinished += SelectActer;
        FightManager.Instance.WhatUserAndSelectedSkill += SetTargetingChar;
        FightManager.Instance.WhatTargetEntering += MultifulTargeting;
        FightManager.Instance.OnTargetFinded += MakeSkillContext;
    }

    private void OnDisable() // SetActive되는 옵젝이 아니면 이렇게 안해도 되긴 함.
    {
        FightManager.Instance.OnTurnStart -= FightTurnSetting;
        FightManager.Instance.OnActingFinished -= SelectActer;
        FightManager.Instance.WhatUserAndSelectedSkill -= SetTargetingChar;
        FightManager.Instance.WhatTargetEntering -= MultifulTargeting;
        FightManager.Instance.OnTargetFinded -= MakeSkillContext;
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

        SelectActer();
    }

    void SelectActer()
    {
        nowSelectedChar = actingCharacterList[nowSelectedNum];
        nowSelectedNum++;

        CharacterSelected characterSelected = new CharacterSelected
        {
            selectedCharacter = nowSelectedChar
        };

        FightManager.Instance.WhatSelcetedActingChar?.Invoke(characterSelected);
        FightManager.Instance.OnActingCharSelceted?.Invoke();
        GameEvent.OnNoticedSomething($"{nowSelectedChar.characterName}의 차례!");
    }

    void SetTargetingChar(Character user, Skill skill)
    {
        foreach(Character character in actingCharacterList)
        {
            switch(skill)
            {
                case ITargetedOurSkill ourTarget:
                    if(character.iOurUnit)
                    {
                        character.iTargeting = skill.CanCharacterTargeting(character);
                    }
                    break;
                case ITargetedEnemySkill enemyTarget:
                    if(!character.iOurUnit)
                    {
                        character.iTargeting = skill.CanCharacterTargeting(character);
                    }
                    break;
                case ITargetedMeSkill meSkill:
                    if(user.speed == character.speed)
                    {
                        character.iTargeting = skill.CanCharacterTargeting(character);
                    }
                    break;
            }
        }

        this.user = user;
        useSkill = skill;
    }

    void MultifulTargeting(Character eneteringCharacter)
    {
        if(useSkill.skillTargetCount <= 1)
        {
            eneteringCharacter.iSelecting = true;
            return;
        }

        List<Character> targetingList = new();

        foreach(Character character in actingCharacterList)
        {
            if(character.iTargeting)
            {
                targetingList.Add(character);
            }
        }

        int loopNum = targetingList.Count - useSkill.skillTargetCount;
        if(loopNum < 0)
        {
            loopNum = 0;
        }

        for(int i = 0; i < loopNum; i++)
        {
            int r = Random.Range(0, targetingList.Count);

            targetingList.RemoveAt(r);
        }
        
        foreach(Character character in targetingList)
        {
            character.iSelecting = true;
        }

        targets = targetingList;
    }

    SkillContext MakeSkillContext()
    {
        SkillContext context = new SkillContext
        {
            user = this.user,
            useSkill = this.useSkill,
            targets = this.targets
        };

        return context;
    }
}
