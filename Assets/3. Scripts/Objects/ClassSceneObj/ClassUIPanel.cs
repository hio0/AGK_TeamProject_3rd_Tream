using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassUIPanel : MonoBehaviour
{
    [SerializeField] CanvasGroup can;

    // Start is called before the first frame update
    void OnEnable()
    {
        SchoolManager.instance.OnStarted += Act;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnStarted -= Act;
    }

    void Act()
    {
        can.alpha = 0f;
        UIMovement.DOFade(can, 1f, 1.5f);
    }
}
