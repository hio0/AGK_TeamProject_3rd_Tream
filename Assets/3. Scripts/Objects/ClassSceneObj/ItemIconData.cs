using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconData : MonoBehaviour
{
    [SerializeField] BasicIcon icon;
    public Item myItem { get; private set; }

    public void Initialize(Item item)
    {
        SetValue(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetValue(Item data)
    {
        myItem = data;

        icon.spriteImage.sprite = data.data.itemImage;
    }

    public void RemoveVal()
    {
        myItem = null;

        icon.spriteImage.sprite = null;
    }
}
