using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] RectTransform miniMapRange;
    [SerializeField] Transform parent_map;

    MapData map;

    // Start is called before the first frame update
    void Start()
    {
        SetMap();
        InputManager.Instance.OnPressingA += RotateToLeft;
        InputManager.Instance.OnPressingD += RotateToRight;
        FightManager.Instance.OnFighting += Active;
        FightManager.Instance.OnFightFinish += Active;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnPressingA -= RotateToLeft;
        InputManager.Instance.OnPressingD -= RotateToRight;
        FightManager.Instance.OnFighting -= Active;
        FightManager.Instance.OnFightFinish -= Active;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMap()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();
        map = data.floorRoomList[data.nowFloor].GetComponent<MapData>();

        for(int i = 0; i < parent_map.childCount; i++)
        {
            Destroy(parent_map.GetChild(i));
        }

        MapData mpData = Instantiate(map, parent_map);
        mpData.Initialize(data.nowFloor);
    }

    void RotateToLeft()
    {
        Rotate(false);
    }

    void RotateToRight()
    {
        Rotate(true);
    }

    void Rotate(bool isright)
    {
        Vector3 vec = new();
        int z = -1;

        if (isright)
        {
            z = 1;
        }

        vec = new Vector3(0, 0, z);
        miniMapRange.Rotate(vec * 50f * Time.deltaTime);
    }

    void Active()
    {
        bool active = gameObject.activeSelf;

        gameObject.SetActive(!active);
    }
}
