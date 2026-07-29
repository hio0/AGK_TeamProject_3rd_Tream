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

        Action setNum = () =>
        {
            numText.text = mychar.speed.ToString();
            numText.color = new Color32(171, 171, 171, 255);
        };

        FightManager.Instance.OnTurnStart -= setNum;
        FightManager.Instance.OnTurnStart += setNum;

        Action<CharacterSelected> act = (selectedChar) =>
        {
            if(selectedChar.selectedCharacter.speed > mychar.speed)
            {
                numText.color = new Color32(102, 102, 102, 255);
            }
        };
    }
}
