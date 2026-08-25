using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryInfo : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float speed;
    bool isIn;

    [SerializeField] ItemIcon pre_icon;
    [SerializeField] Transform parent_icon;
    [SerializeField] GameObject noticeT;
    [SerializeField] EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        CreateInventory();
        rect.anchoredPosition = closePos;

        Templet.AddEvent(trigger, EventTriggerType.Drop, OnDrop);
    }

    private void OnDisable()
    {
        trigger.triggers.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (isIn)
        {
            UIMovement.DoAnchorMove(rect, closePos, speed);
        }
        else
        {
            UIMovement.DoAnchorMove(rect, openPos, speed);
        }

        isIn = !isIn;
    }

    void CreateInventory()
    {
        IEnumerator Cor()
        {
            for (int i = 0; i < ImportantData.myItemList.Count; i++)
            {
                Instantiate(pre_icon, parent_icon);

                // 10개 만들 때마다 한 프레임 쉬기
                if (i % 10 == 0)
                    yield return null;
            }
        }

        StartCoroutine(Cor());
    }

    void SetInven()
    {
        if (ImportantData.myItemList.Count == 0)
        {
            noticeT.SetActive(true);
        }
        else
        {
            noticeT.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemIcon drag = eventData.pointerDrag.GetComponent<ItemIcon>();

        ItemManager.Instance.OnRemoveItem(drag.myItem, -1);
        drag.Initialize(drag.count - 1, drag.myItem);

        bool isin = false;
        for(int i = 0; i < parent_icon.childCount; i++)
        {
            ItemIcon icon = parent_icon.GetChild(i).GetComponent<ItemIcon>();

            if(icon.myItem == drag.myItem)
            {
                ImportantData.myItemList[drag.myItem] += 1;
                icon.Initialize(icon.count + 1, icon.myItem);

                isin = true;
                break;
            }
        }

        if(!isin)
        {
            ImportantData.myItemList.Add(drag.myItem, drag.count);
            ItemIcon icon = Instantiate(pre_icon, parent_icon);

            icon.Initialize(drag.count, drag.myItem);
        }

        SetInven();
    }
}
