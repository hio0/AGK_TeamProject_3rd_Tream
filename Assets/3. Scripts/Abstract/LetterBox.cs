using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LetterBox : MonoBehaviour
{
    protected RectTransform rect;
    [SerializeField] protected Vector2 deffultTargetingPos;
    [SerializeField] protected float animationSpeed;
    protected bool isIn;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(0, 0);
        isIn = false;
    }

    protected void Move()
    {
        StopAllCoroutines();

        Vector2 targetPos = Vector2.zero;
        if(!isIn)
        {
            targetPos = deffultTargetingPos;
        }
        Debug.Log($"sf {rect != null}");
        isIn = !isIn;
        StartCoroutine(UIMovement.SizeSetAnimation(rect, targetPos, animationSpeed));
    }
}
