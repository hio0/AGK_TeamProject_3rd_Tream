using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] BasicIcon icon;
    [SerializeField] TMP_Text countT;

    Item myItem;
    int count;

    public void Initialize(int count, Item item)
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
}
