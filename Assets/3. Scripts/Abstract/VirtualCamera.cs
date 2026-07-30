using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public abstract class VirtualCamera : MonoBehaviour
{
    protected CinemachineCamera cam;

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

    protected virtual void LockMovingCamera(bool enabled)
    {
        TryGetComponent<CinemachineInputAxisController>(out CinemachineInputAxisController axisController);

        axisController.Controllers[0].Enabled = enabled;
        axisController.Controllers[1].Enabled = enabled;
    }
}
