using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Image image;
    [SerializeField] TMP_Text remainItemT;
    [SerializeField] TMP_Text itemNameT;
    [SerializeField] TMP_Text itemTierT;
    [SerializeField] TMP_Text itemExpT;

    int nowRemainCount;
    List<KeyValuePair<ItemData, int>> remainItemList = new();

    public void Initialize(List<KeyValuePair<ItemData, int>> remainItemList)
    {
        obj.SetActive(true);

        nowRemainCount = 0;
        this.remainItemList = remainItemList;

        SetNextItem();
    }

    // Start is called before the first frame update
    void Start()
    {
        SchoolManager.instance.OnItemFind += Initialize;
        SchoolManager.instance.OnNextFind += GetOrTrash;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnItemFind -= Initialize;
        SchoolManager.instance.OnNextFind -= GetOrTrash;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetOrTrash(bool isGet)
    {
        if(isGet)
        {
            ItemManager.Instance.OnAddItem?.Invoke(remainItemList[nowRemainCount].Key, remainItemList[nowRemainCount].Value);
        }
        else
        {
            SchoolManager.instance.OnNoticedSomething?.Invoke($"{remainItemList[nowRemainCount].Key.itemName}은/는\n 가져가지 않기로 했다.");
        }

        SetNextItem();
    }

    void SetNextItem()
    {
        image.sprite = remainItemList[nowRemainCount].Key.itemImage;
        remainItemT.gameObject.SetActive(true);

        Debug.Log(remainItemList.Count);
        Debug.Log(nowRemainCount);
        int a = remainItemList.Count - nowRemainCount - 1;
        if (a <= 0)
        {
            remainItemT.gameObject.SetActive(false);
        }
        remainItemT.text = $"앞으로 <size=40>{a}개</size>";

        string b = null;
        if (remainItemList[nowRemainCount].Value > 1)
        {
            b = $"x{remainItemList[nowRemainCount].Value}";
        }

        itemNameT.text = remainItemList[nowRemainCount].Key.itemName + b;

        string c = null;
        Color32 col = new();

        switch(remainItemList[nowRemainCount].Key.itemTier)
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

        itemExpT.text = remainItemList[nowRemainCount].Key.itemExplanation;

        nowRemainCount++;
        if(nowRemainCount > remainItemList.Count)
        {
            SchoolManager.instance.OnItemFindEnd?.Invoke();
            obj.SetActive(false);
        }
    }
}
