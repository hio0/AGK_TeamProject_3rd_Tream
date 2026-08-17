using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] ItemText pre_text;
    [SerializeField] Transform parent_text;
    [SerializeField] ItemData deduct;

    public Action<Item> OnItemAdded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddItem(deduct, 1);
    }

    public void AddItem(ItemData data, int added)
    {
        Dictionary<Item, int> list = ImportantData.gettingItemList;
        Item addItem = data.myItem;

        if(list.Count != 0 && list.ContainsKey(addItem))
        {
            list[addItem] += added;
        }
        else
        {
            list.Add(addItem, added);
        }
        ItemText text = Instantiate(pre_text, parent_text);
        text.Initialize(data, added);

        OnItemAdded?.Invoke(addItem);
    }
}
