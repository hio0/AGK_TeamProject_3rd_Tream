using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapRange : MonoBehaviour
{
    public TMP_Text floorT;

    public MapIcon pre_mapIcon;
    public Transform parent_mapIcon;
    List<GameObject> mapShape = new();

    RoomData data;
    int myfloor;

    public void Initialize(int myfloor, RoomData data)
    {
        this.myfloor = myfloor;
        this.data = data;
    }

    public List<GameObject> MyData()
    {
        return mapShape;
    }

    // Start is called before the first frame update
    void Start()
    {
        floorT.text = $"{myfloor}층";

        int num = 0;
        foreach (KeyValuePair<int, List<GameObject>> room in data.floorRoomList)
        {
            if (room.Key == myfloor)
            {
                MapIcon icon = Instantiate(pre_mapIcon, parent_mapIcon);
                //icon.Initialize(room.);
                mapShape.Add(icon.gameObject);

                num++;
            }
        }
    }
}
