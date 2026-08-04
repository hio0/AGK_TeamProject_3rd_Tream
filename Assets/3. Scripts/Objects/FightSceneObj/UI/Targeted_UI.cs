using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeted_UI : ActionObject
{
    Character mychar;

    // Start is called before the first frame update
    void Start()
    {
        mychar = GetComponentInParent<Character>();

        Action enter = () =>
        {
            if (mychar.iSelecting)
            {
                can.alpha = 1f;
            }
            else
            {
                can.alpha = 0f;
            }
        };

        Action exit = () =>
        {
            can.alpha = 0f;
        };

        mychar.OnTriggerEnter += enter;
        mychar.OnTriggerExit += exit;

        FightManager.Instance.OnActingFinished += exit;
    }
}
