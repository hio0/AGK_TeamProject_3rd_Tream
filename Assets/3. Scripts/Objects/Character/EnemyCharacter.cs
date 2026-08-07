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
        FocusCamera.Instance.LockingMovingCamera(false);

        FightManager.Instance.OnTargetFinding?.Invoke();
    }

    protected override void CanITargeting()
    {
        targetCharList.Clear();
        usedSkill = null;

        Skill skill = mychar.SkillSetPattern();
        usedSkill = skill;

        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();

        foreach (Character targetchar in rangeData.allCharacterList)
        {
            targetchar.iTargeting = skill.CanCharacterTargeting(mychar, targetchar);

            if (targetchar.iTargeting)
            {
                targetCharList.Add(targetchar);
            }
        }
    }

    protected override void TargetFinding()
    {
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);

            int r = Random.Range(0, targetCharList.Count);

            targetCharList[r].selectingTargets = MultifulTargeting(targetCharList[r], usedSkill);

            skillContext = MakeSkillContext(usedSkill, targetCharList[r].selectingTargets);

            FightManager.Instance.OnTargetFinded?.Invoke();
            targetCharList[r].OnTriggerEnter?.Invoke();
        }
        StartCoroutine(Wait());
    }
}
