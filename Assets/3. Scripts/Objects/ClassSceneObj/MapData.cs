using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public int mapCount;
    public Action OnReseted;

    int myfloor;

    public void Initialize(int myfloor)
    {
        this.myfloor = myfloor;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<MapIcon>().Initialize(i, myfloor, this);
        }
    }
}
