using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapRange : MonoBehaviour
{
    public TMP_Text floorT;

    public Transform parent_mapIcon;
    GameObject map;

    RoomData data;
    int myfloor;

    public void Initialize(int myfloor, RoomData data, GameObject map)
    {
        this.myfloor = myfloor;
        this.data = data;
        this.map = map;
    }

    public GameObject MyData()
    {
        return map;
    }

    // Start is called before the first frame update
    void Start()
    {
        floorT.text = $"{myfloor}층";

        GameObject obj = Instantiate(map, parent_mapIcon);
        obj.GetComponent<MapData>().Initialize(myfloor);
    }
}
