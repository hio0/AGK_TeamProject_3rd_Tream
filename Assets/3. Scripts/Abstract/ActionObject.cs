using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionObject : MonoBehaviour
{
    protected CanvasGroup can;

    private void Awake()
    {
        can = GetComponent<CanvasGroup>();
    }
}
