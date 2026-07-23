using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLetterBox_UI : LetterBox
{
    public UpLetterBox_UI()
    {
        deffultTargetingPos = new Vector2(0, 140f);
        animationSpeed = 3f;
    }

    private void OnEnable()
    {
        Action act = () => Move(true);
        GameEvent.OnNoticedSomething += act;
    }

    private void OnDisable()
    {
        Action act = () => Move(false);
        GameEvent.OnNoticedSomething += act;
    }
}
