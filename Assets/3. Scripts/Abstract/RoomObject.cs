using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomObject : MonoBehaviour
{
    protected RectTransform rect;
    bool isTouchMiddle;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        isTouchMiddle = false;
        Map.Instance.OnMove += Move;
    }

    private void OnDisable()
    {
        Map.Instance.OnMove -= Move;
    }

    public virtual void OnMiddle()
    {

    }

    protected void Move()
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + -370 * Time.deltaTime, rect.anchoredPosition.y);

        int a = (int)Math.Round(rect.anchoredPosition.x) / 100 * 100;
        if (a == -1800 && !isTouchMiddle)
        {
            OnMiddle();
            isTouchMiddle = true;
        }

        if(Math.Round(rect.anchoredPosition.x) >= 5000)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}