using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBox : RoomObject
{
    public List<ItemData> items = new();
    public List<KeyValuePair<ItemData, int>> itemList = new();

    public override void OnMiddle()
    {
        Map.Instance.Stop();
        int r = Random.Range(2, 5);

        itemList.Clear();
        List<int> list = new();

        for (int i = 0; i < r; i++)
        {
            int item = Random.Range(0, items.Count);
            if(list.Contains(item))
            {
                i--;
                continue;
            }

            int count = Random.Range(1, 3);
            itemList.Add(new KeyValuePair<ItemData, int>(items[item], count));
            //list.Add(item);
        }

        IEnumerator Cor()
        {
            yield return new WaitForSeconds(1.5f);

            SchoolManager.instance.OnItemFind?.Invoke(itemList);
            SchoolManager.instance.OnItemFinding?.Invoke();
            SchoolManager.instance.OnNoticedSomething?.Invoke("캐릭터를 선택해\n의견을 결정하자!");
        }

        StartCoroutine(Cor());
    }
}
