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
        FightManager.Instance.OnCharSelceted += SkillIconSet;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnCharSelceted -= SkillIconSet;
    }

    void SkillIconSet(CharacterSelected nowSelectedChar) // 스껄
    {
        if (nowSelectedChar.selectedCharacter.skillList == null)
        {
            return;
        }
        else
        {
            mySkillList = nowSelectedChar.selectedCharacter.skillList;
        }

        foreach(Skill skill in mySkillList)
        {
            SkillIcon skillIcon = Instantiate(pre_skillIcon, transform);
            
            skillIcon.Initialize(skill, skillExplanation);
        }
    }
}
