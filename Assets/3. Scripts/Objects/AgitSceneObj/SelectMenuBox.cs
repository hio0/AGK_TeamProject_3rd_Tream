using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMenuBox : MonoBehaviour
{
    public TMP_Text menuNameT;
    public Transform parent_menuTransform;

    public Vector2 closePos;
    public Vector2 openPos;

    public GameObject exit;

    CanvasGroup can;
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        can = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();

        Sleep();

        AgitManager.instance.OnButtonClicked += Open;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Open(string menuName, GameObject tool)
    {
        can.alpha = 1f;
        can.blocksRaycasts = true;
        exit.SetActive(true);

        UIMovement.DoAnchorMove(rect, openPos, 0.5f);

        menuNameT.text = menuName;

        for (int i = 0; i < parent_menuTransform.childCount; i++)
        {
            Destroy(parent_menuTransform.GetChild(i).gameObject);
        }

        Instantiate(tool, parent_menuTransform);   
    }

    public void Close()
    {
        Sleep();
    }

    void Sleep()
    {
        can.blocksRaycasts = false;
        exit.SetActive(false);

        UIMovement.DoAnchorMove(rect, closePos, 0.5f);
    }
}
