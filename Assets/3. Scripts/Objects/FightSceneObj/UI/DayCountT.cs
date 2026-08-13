using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayCountT : MonoBehaviour
{
    public TMP_Text dayCountT;

    // Start is called before the first frame update
    void Start()
    {
        dayCountT.text = ImportantData.dayCount.ToString();    
    }
}
