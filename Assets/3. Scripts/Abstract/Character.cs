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
    public string characterName;
    public List<SkillData> skillList = new();
    
    public int hp;
    public int speed;
    public int minSpeed;

    [Header("시스템")]
    public int nowPosition;

    [Header("컴포넌트")]
    public Image characterImage;
    public EventTrigger characterTrigger;

    private void Start()
    {
        ReturnToBasic();

        FightManager.Instance.WhatSelcetedActingChar += AnotherSelected;
        FightManager.Instance.OnTargetFinded += ReturnToBasic;
    }

    void AnotherSelected(CharacterSelected selectedChar)
    {
        if(selectedChar.selectedCharacter.speed == speed)
        {
            return;
        }

        characterImage.color = new Color32(116, 116, 116, 200);
        characterTrigger.enabled = false;
    }

    void ReturnToBasic()
    {
        characterImage.color = new Color32(255, 255, 255, 255);
        characterTrigger.enabled = true;
    }
}
