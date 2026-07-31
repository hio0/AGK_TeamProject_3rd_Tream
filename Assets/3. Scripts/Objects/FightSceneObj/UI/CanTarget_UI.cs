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

        Action action = () =>
        {
            if(mychar.iTargeting)
            {
                can.alpha = 1f;
            }
            else
            {
                can.alpha = 0f;
            }
        };

        FightManager.Instance.OnTargetFinding -= action;
        FightManager.Instance.OnTargetFinding += action;
    }
}
