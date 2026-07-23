using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("기본 정보")]
    public string characterName;
    public List<Skill> skillList = new();
    
    public int hp;
    public int speed;
    public int minSpeed;

    [Header("시스템")]
    public int nowPosition;

    private void Start()
    {
        DeffultSetting();
    }

    void CheckMyPosition()
    {
        nowPosition = transform.GetSiblingIndex(); // 자신의 부모 오브젝트 자식 중에서 내가 몇번째인지 구함
    }

    void DeffultSetting()
    {
        CheckMyPosition();
    }
}
