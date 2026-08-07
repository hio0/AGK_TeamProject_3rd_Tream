using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWave : ScriptableObject
{
    public string waveName;

    public float wavePersent;

    public List<Character> enemyList = new();
}
