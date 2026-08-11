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

        mychar.OnActingStart -= Find;
        mychar.OnActingStart += Find;
    }

    void Find()
    {
        /*
        Character charselect = FightManager.Instance.GetRangeData?.Invoke().nowSelectedChar;
        float size = 0f;

        if (mychar.speed == charselect.speed)
        {
            size = 45f;
        }
        else
        {
            size = 30f;
        }

        rect.sizeDelta = new Vector2(size, size);
        */
    }

    private void OnDisable()
    {
        mychar.OnActingStart -= Find;
    }
}
