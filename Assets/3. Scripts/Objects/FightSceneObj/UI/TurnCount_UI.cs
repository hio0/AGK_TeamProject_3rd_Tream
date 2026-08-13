using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnCount_UI : MonoBehaviour
{
    [SerializeField] TMP_Text turnCountT;
    int nowTurn;

    // Start is called before the first frame update
    void OnEnable()
    {
        FightManager.Instance.OnTurnStart += SetTurnCountT;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnTurnStart -= SetTurnCountT;
    }

    void SetTurnCountT()
    {
        nowTurn = FightManager.Instance.turnCount;
        turnCountT.text = $"Turn {nowTurn}";
    }
}
