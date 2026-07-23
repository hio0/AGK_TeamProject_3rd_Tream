using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LetterBox : MonoBehaviour
{
    protected RectTransform rect;
    [SerializeField] protected Vector2 deffultTargetingPos;
    [SerializeField] protected float animationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Move(bool isin)
    {
        UIMovement.SizeSetAnimation(rect, deffultTargetingPos, animationSpeed);
    }
}
