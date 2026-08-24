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
        NeedIcon();
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
        ResetInfo();

        Character nowSelectedChar = FightManager.Instance.GetRangeData?.Invoke().nowSelectedChar;

        if(nowSelectedChar.iOurUnit)
        {
            mySkillList = nowSelectedChar.skillList;

            foreach (SkillData skill in mySkillList)
            {

                SkillIcon skillIcon = Instantiate(pre_skillIcon, transform);

                skillIcon.Initialize(skill, skillExplanation, nowSelectedChar);
            }
        }
    }

    void ResetInfo()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
