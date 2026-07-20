using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_UI : ActionObject
{
    Character mychar;

    // Start is called before the first frame update
    void Start()
    {
        can.alpha = 0f;
        mychar = GetComponentInParent<Character>();

        if(mychar.nowPosition == FightManager.Instance.nowSelecedNum)
        {
            Action action = () => can.alpha = 1f;
            FightManager.Instance.OnCharSelceted += action;
        }
    }
}
