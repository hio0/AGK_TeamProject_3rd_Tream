using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    public TMP_Text roomT;
    public TMP_Text floorT;

    public TMP_Text classT;

    // Start is called before the first frame update
    void Start()
    {
        SchoolManager.instance.OnStarted += SetFloorT;
        SchoolManager.instance.OnNextRoom += NextRoomT;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextRoomT()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();

        roomT.text = data.nowRoom.roomName;
    }

    void SetFloorT()
    {
        RoomData data = SchoolManager.instance.GetRoomData?.Invoke();

        floorT.text = $"{data.nowFloor}F";
    }
}
