using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public abstract class VirtualCamera : MonoBehaviour
{
    protected CinemachineCamera cam;

    protected virtual void Awake()
    {
       cam = GetComponent<CinemachineCamera>();
    }
    // 간편 기능
    public void LivingAndTargeting(Character target)
    {
        SetTarget(target);
        Live(100); // 고정으로 보이게
    }


    // 기능
    public virtual void Live(int priority)
    {
        cam.Priority = priority;
        Debug.Log($"Lived: {priority}");
    }

    public virtual void SetTarget(Character target)
    {
        Transform targetTransform = target.gameObject.transform;

        cam.Target.TrackingTarget = targetTransform;
    }

    public virtual void LockingMovingCamera(bool enabled)
    {
        TryGetComponent<CinemachineInputAxisController>(out CinemachineInputAxisController axisController);

        axisController.Controllers[0].Enabled = enabled;
        axisController.Controllers[1].Enabled = enabled;
    }
}
