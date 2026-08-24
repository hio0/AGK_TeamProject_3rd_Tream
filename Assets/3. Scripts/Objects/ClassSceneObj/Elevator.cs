using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : RoomObject
{
    private void OnEnable()
    {
        SchoolManager.instance.OnNextFloor += Map.Instance.EventSet;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnNextFloor -= Map.Instance.EventSet;
    }

    public override void OnMiddle()
    {
        IEnumerator Cor()
        {
            Map.Instance.Stop();
            Map.Instance.EventDiSet();

            yield return new WaitForSeconds(1.5f);

            ElevatorChange.instance.Move();

            yield return new WaitForSeconds(1.5f);

            ElevatorChange.instance.Canvas(0);

            SchoolManager.instance.OnElevatorScene?.Invoke();
        }

        StartCoroutine(Cor());

    }
}
