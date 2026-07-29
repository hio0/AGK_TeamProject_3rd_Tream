using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public abstract class VirtualCamera : MonoBehaviour
{
    CinemachineCamera cam;

    private void Awake()
    {
       cam = GetComponent<CinemachineCamera>();
    }

    protected virtual void Live(int priority)
    {
        cam.Priority = priority;
    }

    protected virtual void SetTarget(GameObject target)
    {
        Transform targetTransform = target.transform;

        cam.Target.TrackingTarget = targetTransform;
    }
}
