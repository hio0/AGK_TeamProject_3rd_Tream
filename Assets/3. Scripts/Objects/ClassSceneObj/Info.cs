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
        SchoolManager.instance.OnRoomChanged += SetRoomT;
        SchoolManager.instance.OnNextFloor += SetFloorT;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetRoomT(string room)
    {
        roomT.text = room;
    }

    void SetFloorT()
    {
        if(ImportantData.nowFloorCount <= 0)
        {
            floorT.gameObject.SetActive(false);
        }
        else
        {
            floorT.gameObject.SetActive(true);
        }
        floorT.text = $"{ImportantData.nowFloorCount}F";
    }
}
