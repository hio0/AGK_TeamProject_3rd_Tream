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
    public bool iSelecting; // 이건 이벤트버스로 해도 되긴하는데,,,어차피 이거 관여하는 쪽에서 이미 날 알고 있어서, 모르는 채로 정보 교환이라는 이벤트 버스 방식일 필요가 없어서,,,

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
        FightManager.Instance.OnActingFinished += ReturnToBasic;
        FightManager.Instance.OnTargetFinding += SetTriggerTargeting;
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

    void SetTriggerTargeting()
    {
        if(!iTargeting)
        {
            return;
        }

        ReturnToBasic();

        Action<PointerEventData> onEnter = (pointEventData) =>
        {
            FightManager.Instance.WhatTargetEntering?.Invoke(this);

            FightManager.Instance.OnTargetEntering?.Invoke();
            OnTriggerEnter?.Invoke();
        };

        Action<PointerEventData> onClick = (pointEventData) =>
        {
            FightManager.Instance.OnTargetClicked?.Invoke();
            OnTriggerClick?.Invoke();
        };

        Action<PointerEventData> onExit = (pointEventData) =>
        {
            iSelecting = false;

            FightManager.Instance.OnTargetExiting?.Invoke();
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
