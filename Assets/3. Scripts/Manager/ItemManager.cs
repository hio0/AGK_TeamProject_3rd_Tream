using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public Action<ItemData, int> OnAddItem;
    public Action<ItemData, int> OnRemoveItem;

    [SerializeField] ItemText pre_text;
    [SerializeField] Transform parent_text;

    private void Awake()
    {
        Instance = this;

        OnAddItem += AddItem;
        OnRemoveItem += RemoveItem;
    }

    private void OnDisable()
    {
        OnAddItem -= AddItem;
        OnRemoveItem -= RemoveItem;
    }

   void AddItem(ItemData data, int added)
    {
        Dictionary<ItemData, int> list = ImportantData.gettingItemList;

        if(list.Count != 0 && list.ContainsKey(data))
        {
            list[data] += added;
        }
        else
        {
            list.Add(data, added);
        }
        ItemText text = Instantiate(pre_text, parent_text);
        text.Initialize(data, added);

        ImportantData.gettingItemList = list;
    }

    void RemoveItem(ItemData data, int minus)
    {
        Dictionary<ItemData, int> list = ImportantData.gettingItemList;

        if (list.Count != 0 && list.ContainsKey(data))
        {
            list[data] -= minus;

            ItemText text = Instantiate(pre_text, parent_text);
            text.Initialize(data, minus);

            if (list[data] <= 0)
            {
                list.Remove(data);
            }

            ImportantData.gettingItemList = list;
        }
    }
}
