using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] BasicIcon icon;
    [SerializeField] TMP_Text countT;

    public ItemData myItem { get; private set; }
    public int count { get; private set; }

    public void Initialize(int count, ItemData item)
    {
        myItem = item;
        this.count = count;
    }

    // Start is called before the first frame update
    void Start()
    {
        icon.spriteImage.sprite = myItem.itemImage;
        countT.text = count.ToString();
    }

    public void ChangeCount(int value)
    {
        count += value;
        countT.text = count.ToString();
    }
}
