using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeted_UI : ActionObject
{
    Character mychar;
     
    static Action OnExit;

    // Start is called before the first frame update
    void Start()
    {
        mychar = GetComponentInParent<Character>();

        mychar.OnTriggerEnter += Enter;
        mychar.OnTriggerExit += Exit;

        OnExit += ForceExit;

        FightManager.Instance.OnActingFinished += ForceExit;
    }

    void OnDestroy()
    {
        mychar.OnTriggerEnter -= Enter;
        mychar.OnTriggerExit -= Exit;

        OnExit -= ForceExit;

        FightManager.Instance.OnActingFinished -= ForceExit;
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
        OnExit?.Invoke();
    }

    void ForceExit()
    {
        can.alpha = 0f;
    }
}
