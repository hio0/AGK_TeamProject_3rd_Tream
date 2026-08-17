using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    public MapRange pre_map;
    public Transform parent_transform;

    private void OnEnable()
    {
        SchoolManager.instance.OnStarted += MakeMap;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnStarted -= MakeMap;
        SaveData();
    }

    void MakeMap()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();

        for (int i = 0; i < data.floorCount; i++)
        {
            MapRange range = Instantiate(pre_map, parent_transform);
            
            range.Initialize(i + 1, data, data.floorRoomList[i + 1]);

        }
    }

    void SaveData()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();

        for (int i = 0; i < parent_transform.childCount; i++)
        {
            GameObject map;

            MapRange range = parent_transform.GetChild(i).GetComponent<MapRange>();
            map = range.MyData();

            data.floorRoomList[i] = map;
        }
    }
}
