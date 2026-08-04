using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public abstract class Character : MonoBehaviour
{
    [Header("기본 정보")]
    public CharacterData characterData;
    public string characterName;
    public List<Skill> skillList = new();

    public int hp;
    public int speed;
    public List<Flag> flaglist = new();

    public int maxHp;
    public int minSpeed;

    [Header("시스템")]
    public int nowPosition;
    public bool iOurUnit;
    public bool iTargeting;
    public bool iSelecting; // 이건 이벤트버스로 해도 되긴하는데,,,어차피 이거 관여하는 쪽에서 이미 날 알고 있어서, 모르는 채로 정보 교환이라는 이벤트 버스 방식일 필요가 없어서,,,
    public List<Character> selectingTargets = new();

    public Action OnActingStart;
    public Action<Character> OnTargetFinding;
    public Action OnTriggerEnter;
    public Action OnTriggerClick;
    public Action OnTriggerExit;
    public event Action OnAction;
    public event Action OnDamaged;

    [Header("컴포넌트")]
    public Image characterImage;
    public EventTrigger characterTrigger;

    private void OnEnable()
    {
        DefaultSet();
        ReturnToBasic();

        FightManager.Instance.OnActingStart += TurnStartedSet;
        FightManager.Instance.WhatSelcetedActingChar += AnotherSelected;
        FightManager.Instance.WhatUserAndSelectedSkill += CanITargeted;
        FightManager.Instance.OnTargetFinding += Targeting;
        FightManager.Instance.OnTargetFinded += Act;
        FightManager.Instance.OnTurnFinish += ReturnToBasic;
    }

    // 시스템
    void AnotherSelected(Character selectedChar)
    {
        if (selectedChar.speed != speed)
        {
            characterImage.color = new Color32(116, 116, 116, 200);
            return;
        }

        Debug.Log("anoter");
        ReturnToBasic();
        OnActingStart?.Invoke();
    }

    public void ReturnToBasic()
    {
        characterImage.color = new Color32(255, 255, 255, 255);
    }

    public void CanITargeted(Character user, Skill skill)
    {
        iTargeting = skill.CanCharacterTargeting(user, this);
        Debug.Log("camsfn");
    }

    void Targeting()
    {
        OnTargetFinding?.Invoke(this);
    }

    void DefaultSet()
    {
        characterName = characterData.defaultCharacterName;
        skillList = characterData.defaultSkillList;

        maxHp = characterData.defaultHp;
        minSpeed = characterData.defaultMinSpeed;
    }

    void TurnStartedSet()
    {
        selectingTargets.Clear();

        iTargeting = false;
        iSelecting = false;
    }

    // 자식꺼
    public virtual Skill SkillSetPattern() // 기본적인 오토 스킬세팅 ( 특수한 스킬 세팅법이 있는 enemy는 이거 변경해서 씀 ㅇㅇ )
    {
        int r = UnityEngine.Random.Range(0, skillList.Count);

        return skillList[r];
    }

    public virtual void Act(SkillContext skillContext) // 스킬컨텍스트 받고 계산은 스킬 쪽에서 다함 ㅇ
    {
        if(skillContext.user.speed != speed)
        {
            return;
        }

        Debug.Log("act");
        StartCoroutine(skillContext.useSkill.Effected(skillContext));
        OnAction?.Invoke();
    }

    public virtual void Damaged(Action skillEffect)
    {
        skillEffect?.Invoke();
        OnDamaged?.Invoke();
    }
}
