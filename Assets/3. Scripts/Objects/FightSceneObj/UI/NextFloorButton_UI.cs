using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextFloorButton_UI : MonoBehaviour
{
    public TMP_Text floorT;
    public TMP_Text nowFloorT;

    // Start is called before the first frame update
    void Start()
    {
        floorT.text = $"{ImportantData.nowFloorCount}F > {ImportantData.nowFloorCount + 1}F";
        nowFloorT.text = $"{ImportantData.nowFloorCount}F 엘리베이터 | 앞으로 {ImportantData.maxFloorCount - ImportantData.nowFloorCount}F";
    }
}
