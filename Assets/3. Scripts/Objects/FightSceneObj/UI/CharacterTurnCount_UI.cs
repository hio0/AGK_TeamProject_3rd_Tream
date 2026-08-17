using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTurnCount_UI : ActionObject
{
    Character mychar;

    TMP_Text numText;

    // Start is called before the first frame update
    void Start()
    {
        mychar = GetComponentInParent<Character>();
        numText = GetComponent<TMP_Text>();


        UIObject();

        FightManager.Instance.OnActingStart -= SetNum;
        FightManager.Instance.OnActingStart += SetNum;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnActingStart -= SetNum;
    }

    void SetNum()
    {
        numText.text = mychar.nowTurnCount.ToString();
        numText.color = new Color32(171, 171, 171, 255);
    }
}
