using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgitUIPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AgitManager.instance.GetUIRect += ReturnAgit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    RectTransform ReturnAgit()
    {
        return GetComponent<RectTransform>();
    }
}
