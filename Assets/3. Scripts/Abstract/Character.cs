using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public event Action OnTriggerEnter;
    public event Action OnTriggerClick;
    public event Action OnTriggerExit;
    public event Action OnAction;
    public event Action OnDamaged;

    [Header("컴포넌트")]
    public Image characterImage;
    public EventTrigger characterTrigger;

    private void OnEnable()
    {
        DefaultSet();
        ReturnToBasic();

        FightManager.Instance.WhatSelcetedActingChar += AnotherSelected;
        FightManager.Instance.WhatUserAndSelectedSkill += SetTriggerTargeting;
        FightManager.Instance.OnActingFinished += ReturnToBasic;
    }

    // 시스템
    void AnotherSelected(CharacterSelected selectedChar)
    {
        if (selectedChar.selectedCharacter.speed == speed)
        {
            return;
        }

        characterImage.color = new Color32(116, 116, 116, 200);
    }

    void ReturnToBasic()
    {
        characterImage.color = new Color32(255, 255, 255, 255);
    }

    void SetTriggerTargeting(Character user, Skill skill)
    {
        if(!iTargeting)
        {
            return;
        }

        ReturnToBasic();

        Action<PointerEventData> onEnter = (pointEventData) =>
        {
            OnTriggerEnter?.Invoke();
        };

        Action<PointerEventData> onClick = (pointEventData) =>
        {
            OnTriggerClick?.Invoke();
        };

        Action<PointerEventData> onExit = (pointEventData) =>
        {
            OnTriggerExit?.Invoke();
        };

        Templet.AddEvent(characterTrigger, EventTriggerType.PointerEnter, onEnter);
        Templet.AddEvent(characterTrigger, EventTriggerType.PointerClick, onClick);
        Templet.AddEvent(characterTrigger, EventTriggerType.PointerExit, onExit);
    }

    void DefaultSet()
    {
        characterName = characterData.defaultCharacterName;
        skillList = characterData.defaultSkillList;

        maxHp = characterData.defaultHp;
        minSpeed = characterData.defaultMinSpeed;
    }

    // 자식꺼
    public virtual void Action(SkillContext skillContext) // 스킬컨텍스트 받고 계산은 스킬 쪽에서 다함 ㅇ
    {
        skillContext.useSkill.Effected(skillContext);
        OnAction?.Invoke();
    }

    public virtual void Damaged(Action skillEffect)
    {
        skillEffect();
        OnDamaged?.Invoke();
    }
}
