using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIconSetting : MonoBehaviour
{
    public List<SkillData> mySkillList;

    [SerializeField] SkillIcon pre_skillIcon;
    [SerializeField] SkillExplanation skillExplanation;

    private void OnEnable()
    {
        SchoolManager.instance.OnStarted += NeedIcon;
        FightManager.Instance.OnTargetFinded += ResetInfo;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnTargetFinded -= ResetInfo;
    }

    void NeedIcon()
    {
        List<Character> list = FightManager.Instance.GetRangeData?.Invoke().ourRangeChar;
        foreach (Character character in list)
        {
            character.OnActingStart -= SkillIconSet;
            character.OnActingStart += SkillIconSet;
        }
    }

    void SkillIconSet()
    {
        ResetInfo(null);

        Character nowSelectedChar = FightManager.Instance.GetRangeData?.Invoke().nowSelectedChar;

        if(nowSelectedChar.iOurUnit)
        {
            mySkillList = nowSelectedChar.skillList;

            foreach (SkillData skill in mySkillList)
            {
                if(skill.skillType == SkillData.actType.emotion_Pokju)
                {
                    continue;
                }
                if (skill.skillType == SkillData.actType.emotion)
                {
                    if(nowSelectedChar.characterEmotion.imotionStack == 0)
                    {
                        continue;
                    }
                }

                SkillIcon skillIcon = Instantiate(pre_skillIcon, transform);

                skillIcon.Initialize(skill, skillExplanation, nowSelectedChar);
            }
        }
    }

    void ResetInfo(SkillContext context)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
