using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTarget_UI : ActionObject
{
    Character mychar;

    // Start is called before the first frame update
    void Start()
    {
        mychar = GetComponentInParent<Character>();

        FightManager.Instance.OnTargetFinding += Targeting;
        FightManager.Instance.OnActingFinished += Act;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnTargetFinding -= Targeting;
        FightManager.Instance.OnActingFinished -= Act;
    }

    void Targeting()
    {
        if (mychar.iTargeting)
        {
            can.alpha = 1f;
        }
    }

    void Act()
    {
        can.alpha = 0f;
    }
}
