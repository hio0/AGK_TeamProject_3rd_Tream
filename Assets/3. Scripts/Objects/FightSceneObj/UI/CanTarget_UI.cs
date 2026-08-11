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

        FightManager.Instance.OnTargetFinding -= Act;
        FightManager.Instance.OnTargetFinding += Act;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnTargetFinding -= Act;
    }

    void Act()
    {
        if (mychar.iTargeting)
        {
            can.alpha = 1f;
        }
        else
        {
            can.alpha = 0f;
        }
    }
}
