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

    public void Initialize(int myfloor, RoomData data, List<GameObject> list)
    {
        this.myfloor = myfloor;
        this.data = data;
        mapShape = list;
    }

    public List<GameObject> MyData()
    {
        return mapShape;
    }

    // Start is called before the first frame update
    void Start()
    {
        floorT.text = $"{myfloor + 1}층";

        if(mapShape != null)
        {
            Debug.Log("unnull");
            foreach(GameObject go in mapShape)
            {
                Instantiate(go, parent_mapIcon);
            }
        }
        else
        {
            mapShape = new();
            int num = 0;
            foreach (KeyValuePair<int, List<GameObject>> room in data.floorRoomList)
            {
                if (room.Key == myfloor)
                {
                    MapIcon icon = Instantiate(pre_mapIcon, parent_mapIcon);
                    List<GameObject> list = room.Value;

                    icon.Initialize(list[num].GetComponent<Room>());
                    mapShape.Add(icon.gameObject);

                    num++;
                }
            }
        }
    }
}
