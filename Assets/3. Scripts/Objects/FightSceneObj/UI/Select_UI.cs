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
        mychar = GetComponentInParent<Character>();

        Action<CharacterSelected> find = (charselect) =>
        {
            if (mychar.speed == charselect.selectedNum)
            {
                can.alpha = 1f;
            }
            else
            {
                can.alpha = 0f;
            }
        };

        FightManager.Instance.OnActingCharSelceted -= find;
        FightManager.Instance.OnActingCharSelceted += find;
    }
}
