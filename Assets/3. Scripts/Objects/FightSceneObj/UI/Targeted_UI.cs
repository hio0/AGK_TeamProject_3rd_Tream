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

        mychar.OnTriggerEnter += Enter;
        mychar.OnTriggerExit += Exit;

        FightManager.Instance.OnActingFinished += Exit;
    }

    private void OnDestroy()
    {
        mychar.OnTriggerEnter -= Enter;
        FightManager.Instance.OnActingFinished -= Exit;
    }

    void Enter()
    {
        if (mychar.iSelecting)
        {
            can.alpha = 1f;
        }
        else
        {
            can.alpha = 0f;
        }
    }

    void Exit()
    {
        can.alpha = 0f;
    }
}
