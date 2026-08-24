using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgitTrigger : RoomObject
{
    private void OnEnable()
    {
        SchoolManager.instance.OnNextClass += Map.Instance.EventSet;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnNextClass -= Map.Instance.EventSet;
    }

    public override void OnMiddle()
    {
        IEnumerator Cor()
        {
            Map.Instance.Stop();
            Map.Instance.EventDiSet();

            yield return new WaitForSeconds(1.5f);

            SceneChange.instance.Move(false);

            yield return new WaitForSeconds(1.5f);

            SchoolManager.instance.NextDay();
            SchoolManager.instance.OnAgitScene?.Invoke();
        }

        StartCoroutine(Cor());
    }
}
