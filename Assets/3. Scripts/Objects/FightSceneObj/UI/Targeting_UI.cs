using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting_UI : ActionObject
{
    // Start is called before the first frame update
    void Start()
    {
        Action<bool> action = (icanTargeted) =>
        {
            if(icanTargeted)
            {
                can.alpha = 1f;
            }
            else
            {
                can.alpha = 0f;
            }
        };

        FightManager.Instance.OnTargetFinded -= action;
        FightManager.Instance.OnTargetFinded += action;
    }
}
