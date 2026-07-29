using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting_UI : ActionObject
{
    // Start is called before the first frame update
    void Start()
    {
        Action action = () =>
        {
            can.alpha = 1f;
        };

        FightManager.Instance.OnTargetFinded -= action;
        FightManager.Instance.OnTargetFinded += action;
    }
}
