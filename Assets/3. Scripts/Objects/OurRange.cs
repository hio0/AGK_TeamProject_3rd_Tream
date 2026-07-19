using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurRange : MonoBehaviour
{
    public int nowSelecedNum;

    // Start is called before the first frame update
    void Start()
    {
        Action a = () =>
        {
            nowSelecedNum -= 1;
            SelectActingCharacter();
        };

        Action d = () =>
        {
            nowSelecedNum += 1;
            SelectActingCharacter();
        };

        InputManager.Instance.OnPressA += a;
        InputManager.Instance.OnPressD += d;

        Action firstSelect = () =>
        {
            nowSelecedNum = transform.childCount - 1;
            SelectActingCharacter();
        };

        FightManager.Instance.OnFightStart += firstSelect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectActingCharacter()
    {

    }
}
