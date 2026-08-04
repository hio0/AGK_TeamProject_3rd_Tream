using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class School_FocusCamera : VirtualCamera
{
    public static School_FocusCamera Instance;

    protected override void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineCamera>();
    }
}
