using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    public MapRange pre_map;
    public Transform parent_transform;

    public Dictionary<int, List<GameObject>> mapData = new();

    // Start is called before the first frame update
    void Start()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();
        Debug.Log(data.floorCount);

        for (int i = 0; i < data.floorCount; i++)
        {
            GameObject range = Instantiate(pre_map.gameObject, parent_transform);

            if(mapData.ContainsKey(i))
            {
                List<GameObject> list = new();
                list = mapData[i];

                range.GetComponent<MapRange>().Initialize(i, data, list);
            }
            else
            {
                range.GetComponent<MapRange>().Initialize(i, data, null);
            }
            
        }
    }

    private void OnDisable()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();

        for (int i = 0; i < parent_transform.childCount; i++)
        {
            List<GameObject> list = parent_transform.GetChild(i).GetComponent<MapRange>().MyData();
            
            if(mapData.ContainsKey(data.nowFloor))
            {
                mapData[data.nowFloor] = list;
            }
            else
            {
                mapData.Add(data.nowFloor, list);
            }

            Destroy(parent_transform.GetChild(i).gameObject);
        }
    }
}
