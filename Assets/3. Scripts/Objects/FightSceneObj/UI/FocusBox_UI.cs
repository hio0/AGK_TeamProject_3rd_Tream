using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusBox_UI : MonoBehaviour
{
    bool isIn;

    RectTransform rect;
    public float maxHeight;
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
        float height = 0;

        if(isIn)
        {
            height = 0;
        }
        else
        {
            height = maxHeight;
        }

        isIn = !isIn;
        UIMovement.DoSizeMove(rect, new Vector2(rect.sizeDelta.x, height), speed);

    }
}
