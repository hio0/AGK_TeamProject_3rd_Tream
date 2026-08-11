using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionObject : MonoBehaviour
{
    protected CanvasGroup can;

    private void Awake()
    {
        can = GetComponent<CanvasGroup>();
        can.alpha = 0f;
    }

    protected void UIObject()
    {
        FightManager.Instance.OnTurnStart -= ReflectAlpha;
        FightManager.Instance.OnTurnStart += ReflectAlpha;

        FightManager.Instance.OnTurnFinish -= ReflectAlpha;
        FightManager.Instance.OnTurnFinish += ReflectAlpha;

        FightManager.Instance.OnFightFinish -= SetAlphaToZero;
        FightManager.Instance.OnFightFinish += SetAlphaToZero;
    }

    private void OnDestroy()
    {
        FightManager.Instance.OnTurnStart -= ReflectAlpha;
        FightManager.Instance.OnTurnFinish -= ReflectAlpha;
        FightManager.Instance.OnFightFinish -= SetAlphaToZero;
    }

    void ReflectAlpha()
    {
        if (can.alpha == 1)
        {
            can.alpha = 0;
        }
        else
        {
            can.alpha = 1;
        }
    }

    void SetAlphaToZero()
    {
        can.alpha = 0;
    }
}