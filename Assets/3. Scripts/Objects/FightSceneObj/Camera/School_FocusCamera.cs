using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School_FocusCamera : VirtualCamera
{
    // Start is called before the first frame update
    void Start()
    {
        Action iLive = () => Live(100);
        Action iBangJong = () => Live(0); // 뱅종
        iBangJong();

        FightManager.Instance.OnFightStart += iBangJong;

        Action<CharacterSelected> targetFind = (selectedChar) => SetTarget(selectedChar.selectedCharacter.gameObject);
        FightManager.Instance.WhatSelcetedActingChar += targetFind;
        FightManager.Instance.OnActingCharSelceted += iLive;

        FightManager.Instance.OnTargetFinding += iBangJong;
    }
}
