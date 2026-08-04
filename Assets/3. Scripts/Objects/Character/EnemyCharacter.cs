using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCharacter : CharacterTeam
{
    protected override void ActingStart()
    {
        Skill myskill = mychar.SkillSetPattern();

        School_FocusCamera.Instance.Live(0);
        School_FocusCamera.Instance.LockingMovingCamera(false);

        FightManager.Instance.WhatUserAndSelectedSkill?.Invoke(mychar, myskill);
        FightManager.Instance.OnTargetFinding?.Invoke();
    }

    protected override void TargetFinding(Character targetchar)
    {
        CharacterRangeData rangeData = FightManager.Instance.GetRangeData?.Invoke();
        if(rangeData.nowSelectedChar.speed != targetchar.speed)
        {
            return;
        }
        Skill myskill = mychar.SkillSetPattern();

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);

            Debug.Log("waitEnd");
            bool iFindTarget = false;
            while (!iFindTarget)
            {
                int r = Random.Range(0, rangeData.allCharacterList.Count);

                if (rangeData.allCharacterList[r].iTargeting)
                {
                    mychar.selectingTargets = MultifulTargeting(rangeData.allCharacterList[r], myskill);
                    iFindTarget = true;
                }

                yield return null;
            }

            SkillContext context = MakeSkillContext(myskill, mychar.selectingTargets);
            FightManager.Instance.OnTargetFinded?.Invoke(context);
        }
        StartCoroutine(Wait());
    }
}
