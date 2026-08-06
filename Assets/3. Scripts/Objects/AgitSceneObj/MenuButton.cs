using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public string opendMenuName;
    public GameObject opendTool;

    public void OnClick()
    {
        AgitManager.instance.OnButtonClicked?.Invoke(opendMenuName, opendTool);
    }
}
