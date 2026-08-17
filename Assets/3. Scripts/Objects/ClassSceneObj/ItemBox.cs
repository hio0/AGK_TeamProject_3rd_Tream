using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public RectTransform rect;
    public EventTrigger trigger;
    public Image image;

    public List<ItemData> itemList;
    public bool isOpend { get; private set; }

    public void Initialize(Vector2 size, Color32 color, List<ItemData> objs)
    {
        rect.sizeDelta = size;
        image.color = color;
        itemList = objs;

        isOpend = false;
    }

    private void Start()
    {
        FightManager.Instance.OnFighting += TriggerDis;
        FightManager.Instance.OnFightFinish += TriggerEna;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnFighting -= TriggerDis;
        FightManager.Instance.OnFightFinish -= TriggerEna;
    }

    public void OnClick()
    {
        if(isOpend)
        {
            return;
        }

        int succsess = Random.Range(1, 101);
        int plus = Random.Range(15, 41);
        int r = Random.Range(1, 101) + plus;

        if(succsess <= r)
        {
            int itemNum = Random.Range(0, itemList.Count);
            ItemManager.Instance.AddItem(itemList[itemNum], 1);
        }

        isOpend = true;
    }

    public void Exit()
    {
        isOpend = true;
    }

    void TriggerDis()
    {
        trigger.enabled = false;
    }

    void TriggerEna()
    {
        trigger.enabled = true;
    }

}
