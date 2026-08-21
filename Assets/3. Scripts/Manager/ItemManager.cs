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
    public Func<List<ItemIcon>> GetInventory;

    [SerializeField] ItemText pre_text;
    [SerializeField] Transform parent_text;

    private void Awake()
    {
        Instance = this;

    }
}
