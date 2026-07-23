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

        Action find = () =>
        {
            if (mychar.nowPosition == FightManager.Instance.nowSelecedNum)
            {
                can.alpha = 1f;
            }
            else
            {
                can.alpha = 0f;
            }
        };

        FightManager.Instance.OnCharSelceted -= find;
        FightManager.Instance.OnCharSelceted += find;
    }
}
