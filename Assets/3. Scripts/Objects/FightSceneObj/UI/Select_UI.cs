using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Select_UI : ActionObject
{
    Character mychar;

    Image image;
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        mychar = GetComponentInParent<Character>();
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        UIObject();

        Action<CharacterSelected> find = (charselect) =>
        {
            Color32 col = new();
            float size = 0f;

            if (mychar.speed == charselect.selectedCharacter.speed)
            {
                col = new Color32(46, 46, 46, 255);
                size = 45f;
            }
            else
            {
                col = new Color32(46, 46, 46, 255);
                size = 30f;
            }

            image.color = col;
            rect.sizeDelta = new Vector2(size, size);
        };

        FightManager.Instance.WhatSelcetedActingChar -= find;
        FightManager.Instance.WhatSelcetedActingChar += find;
    }
}
