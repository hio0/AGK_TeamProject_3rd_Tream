using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIconSetting : MonoBehaviour
{
    public List<Skill> mySkillList;

    [SerializeField] SkillIcon pre_skillIcon;
    [SerializeField] SkillExplanation skillExplanation;

    private void OnEnable()
    {
        FightManager.Instance.SetSkillIcon += SkillIconSet;
        FightManager.Instance.OnActingFinished += ResetInfo;
    }

    private void OnDisable()
    {
        FightManager.Instance.SetSkillIcon -= SkillIconSet;
        FightManager.Instance.OnActingFinished += ResetInfo;
    }

    void SkillIconSet(Character nowSelectedChar) // 스껄
    {
        mySkillList = nowSelectedChar.skillList;

        foreach (Skill skill in mySkillList)
        {
            SkillIcon skillIcon = Instantiate(pre_skillIcon, transform);

            skillIcon.Initialize(skill, skillExplanation, nowSelectedChar);
        }
    }

    void ResetInfo()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
