using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCharacter : CharacterTeam
{
    protected override void ActingStart()
    {
        FocusCamera.Instance.Live(0);
        FocusCamera.Instance.LockingMovingCamera(false);

        FightManager.Instance.OnTargetFinding?.Invoke();
    }

    protected override void CanITargeting()
    {
        Character user = FightManager.Instance.GetRangeData?.Invoke().nowSelectedChar;
        Skill skill = mychar.SkillSetPattern();

        mychar.iTargeting = skill.CanCharacterTargeting(user, mychar);
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
