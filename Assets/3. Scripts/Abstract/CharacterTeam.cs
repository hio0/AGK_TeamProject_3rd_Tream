using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterTeam : MonoBehaviour
{
    protected Character mychar;
    protected List<Character> targetCharList = new();
    protected SkillContext skillContext;

    private void Awake()
    {
        mychar = GetComponent<Character>();
        ResetTeamEvent();

        mychar.OnActingStart += ActingStart;
        mychar.OnCanITargeted += CanITargeting;
        mychar.OnTargetFinding += TargetFinding;
    }

    public SkillContext RetrunContext()
    {
        return skillContext;
    }

    protected List<Character> MultifulTargeting(Character mainTarget, Skill useSkill) // 타겟 후보들 중 진짜 타겟 정하기
    {
        if (useSkill.skillTargetCount <= 1)
        {
            List<Character> list = new();
            list.Add(mainTarget);

            SelectingTrue(list);
            return list;
        }
        if (mainTarget.selectingTargets.Count != 0)
        {
            SelectingTrue(mainTarget.selectingTargets);
            return mainTarget.selectingTargets;
        }

        List<Character> targetingList = new();
        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();
        foreach (Character character in rangeData.allCharacterList)
        {
            if (character.iTargeting)
            {
                targetingList.Add(character);
            }
        }

        int loopNum = targetingList.Count - useSkill.skillTargetCount;
        if (loopNum < 0)
        {
            loopNum = 0;
        }

        for (int i = 0; i < loopNum; i++)
        {
            int r = UnityEngine.Random.Range(0, targetingList.Count);

            if (targetingList[r].speed == mainTarget.speed)
            {
                i--;
                continue;
            }
            targetingList.RemoveAt(r);
        }

        SelectingTrue(targetingList);

        return new List<Character>(targetingList);

        void SelectingTrue(List<Character> list)
        {
            foreach (Character character in list)
            {
                character.iSelecting = true;
                character.OnTriggerEnter?.Invoke();
                character.ReturnToBasic();
            }
        }
    }

    protected SkillContext MakeSkillContext(Skill skill, List<Character> targets)
    {
        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();

        Debug.Log(targets.Count);
        Debug.Log(skill.skillExplanation);

        SkillContext context = new SkillContext
        {
            user = rangeData.nowSelectedChar,
            useSkill = skill,
            targets = targets,
            ourRangeChar = rangeData.ourRangeChar,
            enemyRangeChar = rangeData.enemyRangeChar
        };

        return context;
    }

    protected virtual void ResetTeamEvent()
    {
        mychar.OnActingStart = null;
        mychar.OnTargetFinding = null;
    }

    protected abstract void ActingStart(); // 어떤 방식으로 스킬을 결정하나 및 어떻게 타깃을 구하나 -> character의 iTargeting을 true

    protected abstract void CanITargeting();

    protected abstract void TargetFinding(); // 어떻게 타겟을 선택하나
}
