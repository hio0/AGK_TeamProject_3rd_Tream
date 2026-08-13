using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpLetterBox_UI : LetterBox
{
    public static UpLetterBox_UI Instance;

    public TMP_Text narrationT;
    public CanvasGroup narrationT_CnGr;
    public float watingTime;
    float timer;

    private void OnEnable()
    {
        deffultTargetingPos = new Vector2(0, 140f);
        animationSpeed = 5f;
        watingTime = 4f;

        SchoolManager.instance.OnNoticedSomething += MyMovingTime;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnNoticedSomething -= MyMovingTime;
    }

    protected override void SetStatic()
    {
        Instance = this;
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

    public void MoveTo(bool lin)
    {
        timer = 0;
        isIn = lin;
        Move();
    }

    void TimerReset()
    {
        timer = watingTime;
    }

    void SetNarrationText(string narration)
    {
        narrationT_CnGr.alpha = 0f;
        narrationT_CnGr.DOKill();

        UIMovement.DOFade(narrationT_CnGr, 1f, 1.5f);
        narrationT.text = narration;
    }
}
