using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance;
    public Camera cam;

    private void Awake()
    {
        Instance = this;

        cam = GetComponent<Camera>();
    }
}
