using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContext
{
    public ItemData data;
    public Character target;
}

[Serializable]
public abstract class Item
{
    public ItemData data;
    public Character target;

    public Action<Item> OnItemEffected;

    public void Initialize(ItemContext context)
    {
        data = context.data;
        target = context.target;
    }

    public virtual void Effect()
    { }
}
