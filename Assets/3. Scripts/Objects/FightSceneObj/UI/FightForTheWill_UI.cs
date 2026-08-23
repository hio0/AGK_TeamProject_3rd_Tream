using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightForTheWill_UI : ActionObject
{
    // Start is called before the first frame update
    void Start()
    {
        Act();
    }

    private void OnDisable()
    {
        
    }

    void Act()
    {
        IEnumerator Cor()
        {
            can.alpha = 1;

            yield return new WaitForSeconds(0.5f);

            UIMovement.DOFade(can, 0f, 0.5f);
        }

        StartCoroutine(Cor());
    }
}
