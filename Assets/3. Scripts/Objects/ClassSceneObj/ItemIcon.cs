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

        if (myItem == null)
        {
            NullValue();
        }
        else
        {
            countT.gameObject.SetActive(true);

            icon.spriteImage.sprite = myItem.itemImage;
            countT.text = count.ToString();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        NullValue();
    }

    void NullValue()
    {
        icon.spriteImage.sprite = null;
        countT.gameObject.SetActive(false);
    }
}
