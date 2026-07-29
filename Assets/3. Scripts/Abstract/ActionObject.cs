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
        FightManager.Instance.OnTurnStart -= ReflectAlpha;
        FightManager.Instance.OnTurnStart += ReflectAlpha;

        FightManager.Instance.OnTurnFinish -= ReflectAlpha;
        FightManager.Instance.OnTurnFinish += ReflectAlpha;
    } 
}