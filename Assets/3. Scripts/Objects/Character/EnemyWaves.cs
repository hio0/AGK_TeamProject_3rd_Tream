using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : RoomObject
{
    public string waveName;
    public List<Character> enemyList = new();

    private void Start()
    {
        FightManager.Instance.GetNowEnemys += ReturnData;
    }

    private void OnDisable()
    {
        FightManager.Instance.GetNowEnemys -= ReturnData;
    }

    EnemyWaves ReturnData()
    {
        return this;
    }

    public override void OnMiddle()
    {
        Map.Instance.Stop();

        FightManager.Instance.OnFighting?.Invoke();
    }
}
