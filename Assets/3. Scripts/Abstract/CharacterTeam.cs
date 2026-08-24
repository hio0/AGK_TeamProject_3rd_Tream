using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterTeam : MonoBehaviour
{
    protected Character mychar;
    protected SkillContext skillContext;

    protected virtual void Awake()
    {
        mychar = GetComponent<Character>();
        mychar.characterTeam = this;
        ResetTeamEvent();

        mychar.OnActingStart += ActingStart;
        mychar.OnCanITargeted += CanITargeting;
        mychar.OnTargetFinding += TargetFinding;
        mychar.OnDied += Dying;
    }

    public SkillContext RetrunContext()
    {
        return skillContext;
    }

    protected List<Character> MultifulTargeting(Character mainTarget, Skill useSkill) // 타겟 후보들 중 진짜 타겟 정하기
    {
        Debug.Log($"mainTarget : {mainTarget}");
        Debug.Log($"useSkill : {useSkill}");
        Debug.Log($"useSkill.data : {(useSkill != null ? useSkill.data : null)}");


        if (useSkill.data.skillTargetCount <= 1)
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

        int loopNum = targetingList.Count - useSkill.data.skillTargetCount;
        if (loopNum < 0)
        {
            loopNum = 0;
        }

        for (int i = 0; i < loopNum; i++)
        {
            int r = UnityEngine.Random.Range(0, targetingList.Count);

            if (targetingList[r].nowTurnCount == mainTarget.nowTurnCount)
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
        mychar.OnActingStart -= ActingStart;
        mychar.OnCanITargeted -= CanITargeting;
        mychar.OnTargetFinding -= TargetFinding;
        mychar.OnDied -= Dying;
    }

    protected abstract void ActingStart(); // 어떤 방식으로 스킬을 결정하나 및 어떻게 타깃을 구하나 -> character의 iTargeting을 true

    protected abstract void CanITargeting();

    protected abstract void TargetFinding(); // 어떻게 타겟을 선택하나

    protected abstract void Dying(); // 죽을 때 어캐하냐 ( 죽음 극복 같은 패시브 있는 놈들 있으면 여기보단 character들 있는데에서 하는게 맞지만 귀찮 )
}
