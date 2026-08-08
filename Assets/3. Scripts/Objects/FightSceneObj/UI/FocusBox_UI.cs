using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusBox_UI : MonoBehaviour
{
    bool isIn;

    RectTransform rect;
    public float maxWidth;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();

        FightManager.Instance.OnTargetFinded += Move;
        FightManager.Instance.OnActingFinished += Move;

        isIn = false;
        rect.sizeDelta = Vector2.zero;
    }

    void Move()
    {
        StopAllCoroutines();

        float width = 0;

        if(isIn)
        {
            width = 0;
        }
        else
        {
            width = maxWidth;
        }

        isIn = !isIn;
        StartCoroutine(UIMovement.SizeSetAnimation(rect, new Vector2(width, rect.sizeDelta.y), speed));                                                                          
    }
}
