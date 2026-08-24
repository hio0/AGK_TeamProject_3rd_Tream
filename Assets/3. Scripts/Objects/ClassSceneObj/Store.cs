using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject store;
    [SerializeField]  List<ItemData> itemList;
    [SerializeField][TextArea] List<string> openSpeak;
    [SerializeField][TextArea] List<string> buySpeak;
    [SerializeField][TextArea] List<string> nonMoney;
    [SerializeField][TextArea] List<string> nonReset;
    [SerializeField] TMP_Text textSpeak;
 
    [SerializeField] Image mage;
    [SerializeField] TMP_Text itemNameT;
    [SerializeField] TMP_Text itemTierT;
    [SerializeField] TMP_Text itemExpT;
    [SerializeField] TMP_Text dongT;
    [SerializeField] TMP_Text resetT;

    int nowSelcelted;
    int resetCount;
    bool isSet;

    // Start is called before the first frame update
    void Start()
    {
        resetCount = 4;

        SetNew();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetItem()
    {
        mage.sprite = itemList[nowSelcelted].itemImage;
        itemNameT.text = itemList[nowSelcelted].itemName;

        string c = null;
        Color32 col = new();
        switch (itemList[nowSelcelted].itemTier)
        {
            case ItemData.tier.common:
                c = "흔해빠진";
                col = new Color32(119, 255, 159, 255);
                break;
            case ItemData.tier.rare:
                c = "평범한";
                col = new Color32(120, 191, 255, 255);
                break;
            case ItemData.tier.hero:
                c = "희귀한";
                col = new Color32(234, 119, 255, 255);
                break;
            case ItemData.tier.legendary:
                c = "전설적인";
                col = new Color32(255, 223, 119, 255);
                break;
        }
        itemTierT.text = c;
        itemTierT.color = col;

        itemExpT.text = itemList[nowSelcelted].itemExplanation;
        dongT.text = itemList[nowSelcelted].dong.ToString();

        resetT.text = $"새로고침({resetCount})";
    }

    public void Buy()
    {
        if(ImportantData.moneyCount >= itemList[nowSelcelted].dong)
        {
            SchoolManager.instance.OnMoneyChanged.Invoke(-itemList[nowSelcelted].dong);
            ItemManager.Instance.OnAddItem.Invoke(itemList[nowSelcelted], 1);

            SetNew();
            SetItem();
        }
        else
        {
            SetText(nonMoney);
            return;
        }
    }

    public void Re()
    {
        if (resetCount <= 0)
        {
            SetText(nonReset);
            return;
        }
        resetCount--;
        if (resetCount <= 0)
        {
            resetCount = 0;
        }

        SetNew();
        SetItem();
    }

    void SetNew()
    {
        nowSelcelted = Random.Range(0, itemList.Count);
    }

    public void StoreSet()
    {
        if (isSet)
        {
            store.SetActive(false);
        }
        else
        {
            if (resetCount <= 0)
            {
                return;
            }
            store.SetActive(true);
            SetItem();
            SetText(openSpeak);
        }

        isSet = !isSet;
    }

    void SetText(List<string> list)
    {
        StopAllCoroutines();

        string speak = list[Random.Range(0, list.Count)];
        StartCoroutine(UIMovement.Typing(textSpeak, speak, 0.05f));
    }
}
