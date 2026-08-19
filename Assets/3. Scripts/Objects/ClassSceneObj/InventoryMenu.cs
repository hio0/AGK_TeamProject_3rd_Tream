using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] GameObject exit;
    [SerializeField] ItemIcon pre_itemIcon;
    [SerializeField] Transform parent_itemIcon;

    bool isIn;
    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float speed;
    List<ItemIcon> iconList = new();

    private void Awake()
    {
        StartSet();
    }

    private void OnEnable()
    {
        isIn = false;

        InputManager.Instance.OnPressTab += OpenInventory;
        ItemManager.Instance.OnAddItem += SetInventory;
        ItemManager.Instance.OnRemoveItem += RemoveInventory;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnPressTab -= OpenInventory;
        ItemManager.Instance.OnAddItem -= SetInventory;
        ItemManager.Instance.OnRemoveItem -= RemoveInventory;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartSet()
    {
        foreach(var item in ImportantData.gettingItemList)
        {
            SetInventory(item.Key, item.Value);
        }

        rect.anchoredPosition = closePos;
    }

    public void OpenInventory()
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
        exit.SetActive(isIn);
    }

    void SetInventory(ItemData data, int value)
    {
        bool isIn = false;

        if(iconList.Count != 0)
        {
            foreach (ItemIcon icon in iconList)
            {
                if (icon.myItem == data)
                {
                    isIn = true;

                    icon.ChangeCount(value);
                }
            }
        }

        if (!isIn)
        {
            ItemIcon icon = Instantiate(pre_itemIcon, parent_itemIcon);
            icon.Initialize(value, data);

            iconList.Add(icon);
        }
    }

    void RemoveInventory(ItemData data, int value)
    {
        foreach (ItemIcon icon in iconList)
        {
            if (icon.myItem == data)
            {
                icon.ChangeCount(-value);
                if(icon.count <= 0)
                {
                    iconList.Remove(icon);
                    Destroy(icon);
                }
            }
        }
    }
}
