using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeReference, SubclassSelector] public Item myItem;
    public enum tier
    {
        common,
        hero,
        legendary
    }
    public tier itemTier;
    public string itemName;
    public Sprite itemImage;
    public int maxStack;
    public List<IconData> icons;
    public int dong;

    [TextArea] public string itemExplanation;
}
