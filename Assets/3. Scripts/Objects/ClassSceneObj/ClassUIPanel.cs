using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassUIPanel : MonoBehaviour
{
    [SerializeField] CanvasGroup can;

    // Start is called before the first frame update
    void Start()
    {
        UIMovement.DOFade(can, 1f, 1.5f);
    }
}
