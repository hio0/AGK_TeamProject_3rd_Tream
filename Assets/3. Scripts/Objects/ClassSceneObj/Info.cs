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
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetFloorT()
    {
        floorT.text = $"{ImportantData.nowFloorCount}F";
    }
}
