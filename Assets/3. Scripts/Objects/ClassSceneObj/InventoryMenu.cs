using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public List<ItemIcon> iconList = new();

    [SerializeField] ItemText pre_text;
    [SerializeField] Transform parent_text;

    private void Awake()
    {
        StartSet();
    }

    private void OnEnable()
    {
        ItemManager.Instance.OnAddItem += SetInventory;
        ItemManager.Instance.OnRemoveItem += RemoveInventory;
        ItemManager.Instance.GetInventory += ReturnData;
    }

    private void OnDisable()
    {
        ItemManager.Instance.OnAddItem -= SetInventory;
        ItemManager.Instance.OnRemoveItem -= RemoveInventory;
        ItemManager.Instance.GetInventory -= ReturnData;
    }

    // Update is called once per frame
    void Update()
    {

    }

    List<ItemIcon> ReturnData()
    {
        return iconList;
    }

    void StartSet()
    {
        if (ImportantData.gettingItemList.Count > 0)
        {
            int a = 0;

            foreach (var item in ImportantData.gettingItemList)
            {
                NewItem(iconList[a], item.Key, item.Value);
                a++;
            }
        }
    }

    void SetInventory(ItemData data, int value)
    {
        bool isIn = false;

        foreach (ItemIcon icon in iconList)
        {
            if (icon.myItem == data)
            {
                isIn = true;

                ImportantData.gettingItemList[data] += value;
                icon.Initialize(ImportantData.gettingItemList[data], data);

                break;
            }
        }

        if (!isIn)
        {
            foreach (ItemIcon icon in iconList)
            {
                if (icon.myItem == null)
                {
                    NewItem(icon, data, value);
                    ImportantData.gettingItemList.Add(data, value);
                    break;
                }
            }
        }

        ItemText text = Instantiate(pre_text, parent_text);
        text.Initialize(data, value);
    }

    void NewItem(ItemIcon icon, ItemData data, int value)
    {
        icon.Initialize(value, data);
    }

    void RemoveInventory(ItemData data, int value)
    {
        foreach (ItemIcon icon in iconList)
        {
            if (icon.myItem != null && icon.myItem == data)
            {
                if (ImportantData.gettingItemList[data] - value <= 0)
                {
                    icon.Initialize(0, null);
                    ImportantData.gettingItemList.Remove(data);
                }
                else
                {
                    ImportantData.gettingItemList[data] += value;
                    icon.Initialize(ImportantData.gettingItemList[data], data);
                }
            }

            ItemText text = Instantiate(pre_text, parent_text);
            text.Initialize(data, value);
        }
    }
}
