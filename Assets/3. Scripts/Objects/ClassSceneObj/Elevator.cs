using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : RoomObject
{

    public override void OnMiddle()
    {
        IEnumerator Cor()
        {
            Map.Instance.EventDiSet();

            yield return new WaitForSeconds(1.5f);

            SchoolManager.instance.OnElevatorScene?.Invoke();
        }

        StartCoroutine(Cor());

    }
}
