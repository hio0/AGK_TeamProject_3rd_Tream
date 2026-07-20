using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("기본 정보")]
    public string characterName;
    
    public int hp;

    [Header("시스템")]
    public int nowPosition;

    private void Start()
    {
        CheckMyPosition();
    }

    void CheckMyPosition()
    {
        nowPosition = FightManager.Instance.OurRange.GetSiblingIndex();
    }
}
