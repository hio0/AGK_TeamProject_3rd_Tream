using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    public Transform parent_objects;
    public GameObject exit;

    bool isIn;
    public Vector2 closePos;
    public Vector2 openPos;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        InputManager.Instance.OnPressTab += Move;

        isIn = false;
        exit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if(isIn)
        {
            UIMovement.DoAnchorMove(rect, closePos, 0.3f);
        }
        else
        {
            UIMovement.DoAnchorMove(rect, openPos, 0.3f);
        }

        isIn = !isIn;
        exit.SetActive(isIn);
    }

    public void OnClickToggle(GameObject gameObject)
    {
        for (int i = 0; i < parent_objects.childCount; i++)
        {
            parent_objects.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }
}
