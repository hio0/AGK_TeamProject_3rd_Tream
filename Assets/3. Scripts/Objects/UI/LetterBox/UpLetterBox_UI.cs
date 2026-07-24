using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpLetterBox_UI : LetterBox
{
    [SerializeField] TMP_Text narrationT;
    [SerializeField] CanvasGroup narrationT_CnGr;
    [SerializeField] float watingTime;
    float timer;

    private void OnEnable()
    {
        deffultTargetingPos = new Vector2(0, 140f);
        animationSpeed = 3f;
        watingTime = 3f;

        GameEvent.OnNoticedSomething += MyMovingTime;
    }

    private void OnDisable()
    {
        GameEvent.OnNoticedSomething -= MyMovingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIn)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                Move();
            }
        }
    }

    void MyMovingTime(string narration) // 꼼수 ㅎㅎ
    {
        TimerReset();
        if (!isIn)
        {
            Move();
        }
        
        SetNarrationText(narration);
    }

    void TimerReset()
    {
        timer = watingTime;
    }

    void SetNarrationText(string narration)
    {
        float speed = animationSpeed - 0.5f;
        narrationT_CnGr.alpha = 0f;

        narrationT_CnGr.DOKill();
        UIMovement.DOFade(narrationT_CnGr, 1f, speed);
        narrationT.text = narration;
    }
}
