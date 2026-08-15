using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    public ItemIcon pre_itemIcon;
    public Transform parent_itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in ImportantData.gettingItemList)
        {
            ItemIcon icon = Instantiate(pre_itemIcon, parent_itemIcon);
            icon.Initialize(item.Value, item.Key);
        }
    }
}
