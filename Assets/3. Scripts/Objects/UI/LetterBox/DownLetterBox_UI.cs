using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLetterBox_UI : LetterBox
{
    private void OnEnable()
    {
        deffultTargetingPos = new Vector2(0, 240f);
        animationSpeed = 3f;

        FightManager.Instance.OnFightStart += Move;
    }
    private void OnDisable()
    {
        FightManager.Instance.OnFightStart -= Move;
    }
}
