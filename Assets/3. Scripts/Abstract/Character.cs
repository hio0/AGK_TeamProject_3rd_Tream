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
    public int maxHp;
    public int speed;
    public int minSpeed;

    [Header("시스템")]
    public int nowPosition;
    public bool iOurUnit;

    [Header("컴포넌트")]
    public Image characterImage;
    public EventTrigger characterTrigger;

    private void OnEnable()
    {
        DefaultSet();
        ReturnToBasic();

        FightManager.Instance.WhatSelcetedActingChar += AnotherSelected;
        FightManager.Instance.OnTargetFinding += ReturnToBasic;
    }

    // 시스템
    void AnotherSelected(CharacterSelected selectedChar)
    {
        if(selectedChar.selectedCharacter.speed == speed)
        {
            return;
        }

        characterImage.color = new Color32(116, 116, 116, 200);
        characterTrigger.enabled = true;
    }

    void ReturnToBasic()
    {
        characterImage.color = new Color32(255, 255, 255, 255);
        characterTrigger.enabled = false;
    }

    void DefaultSet()
    {
        characterName = characterData.defaultCharacterName;
        skillList = characterData.defaultSkillList;

        maxHp = characterData.defaultHp;
        minSpeed = characterData.defaultMinSpeed;
    }
}
