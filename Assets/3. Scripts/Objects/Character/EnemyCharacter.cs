using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCharacter : CharacterTeam
{
    Skill usedSkill;

    protected override void ActingStart()
    {
        FocusCamera.Instance.Live(0);

        usedSkill = null;

        Skill skill = mychar.SkillSetPattern();
        usedSkill = skill;

        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();

        foreach (Character targetchar in rangeData.allCharacterList)
        {
            targetchar.iTargeting = skill.CanCharacterTargeting(mychar, targetchar);
        }

        FightManager.Instance.OnTargetFinding?.Invoke();
    }

    protected override void CanITargeting()
    {
        
    }

    protected override void TargetFinding()
    {
        IEnumerator TargetFind()
        {
            List<Character> targetCharList = new();
            CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();
            foreach (Character targetchar in rangeData.allCharacterList)
            {
                if (targetchar.iTargeting)
                {
                    targetCharList.Add(targetchar);
                }

                yield return null;
            }

            int r = Random.Range(0, targetCharList.Count);
            Character mainTarget = null;

            mainTarget = targetCharList[r];
            mainTarget.selectingTargets = MultifulTargeting(mainTarget, usedSkill);
            SchoolManager.instance.OnNoticedSomething($"{mychar.characterName}의\n{usedSkill.data.skillName}!");

            yield return new WaitForSeconds(1f);

            skillContext = MakeSkillContext(usedSkill, mainTarget.selectingTargets);
            FightManager.Instance.OnTargetFinded?.Invoke();
        }

        StartCoroutine(TargetFind());
    }

    protected override void Dying()
    {
        FightManager.Instance.OnDyingSomeOne?.Invoke(mychar.gameObject);
    }
}
