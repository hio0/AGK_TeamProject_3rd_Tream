using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("기본 정보")]
    public string skillName;

    [Header("시스템")]
    public Character skillUser;
    public Character skilltarget;

    protected abstract void Effect();
}
