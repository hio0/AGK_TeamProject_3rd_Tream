using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LetterBox : MonoBehaviour
{
    public RectTransform rect;
    public Vector2 deffultTargetingPos;
    public float animationSpeed;
    public bool isIn;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(0, 0);
        isIn = false;

        FightManager.Instance.OnFighting += Move;
        FightManager.Instance.OnFightFinish += Move;

        SetStatic();
    }

    private void OnDisable()
    {
        FightManager.Instance.OnFighting -= Move;
        FightManager.Instance.OnFightFinish -= Move;
    }

    public LetterBox ReturnData()
    {
        return this;
    }

    protected abstract void SetStatic(); 

    public void Move()
    {
        StopAllCoroutines();

        Vector2 targetPos = Vector2.zero;
        if(!isIn)
        {
            targetPos = deffultTargetingPos;
        }

        isIn = !isIn;
        StartCoroutine(UIMovement.SizeSetAnimation(rect, targetPos, animationSpeed));
    }
}
