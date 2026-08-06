using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class FocusCamera : VirtualCamera
{
    public static FocusCamera Instance;

    protected override void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineCamera>();
    }
}
