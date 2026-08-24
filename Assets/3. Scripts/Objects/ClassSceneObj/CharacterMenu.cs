using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float speed;
    [SerializeField] GameObject exit;
    bool isIn;
    List<Character> ourList;
    int nowIndex;

    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text characterNameT;
    [SerializeField] TMP_Text hpT;
    [SerializeField] TMP_Text lvT;
    [SerializeField] TMP_Text speedT;
    [SerializeField] LevelIcon lv;

    [SerializeField] List<ItemIconData> pre_item;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        if (isIn)
        {
            lv.gameObject.SetActive(false);
            UIMovement.DoAnchorMove(rect, closePos, speed);
        }
        else
        {
            UIMovement.DoAnchorMove(rect, openPos, speed);
        }

        isIn = !isIn;
        exit.SetActive(isIn);
    }

    public void SetMenu(int index)
    {
        ourList = FightManager.Instance.GetRangeData?.Invoke().ourRangeChar;
        Character selectedChar = ourList[index];
        nowIndex = index;

        characterImage.sprite = selectedChar.characterData.iconImage;
        characterNameT.text = selectedChar.characterName;
        hpT.text = $"{selectedChar.hp} / {selectedChar.maxHp}";
        lvT.text = $"{selectedChar.level} / {selectedChar.maxLevel}";
        speedT.text = $"{selectedChar.minSpeed} - {selectedChar.maxSpeed}";

        lv.gameObject.SetActive(true);
        lv.Initialize(selectedChar);

        for (int i = 0; i < pre_item.Count; i++)
        {
            pre_item[i].RemoveVal();
        }

        for(int j = 0;j < selectedChar.itemList.Count; j++)
        {
            pre_item[j].Initialize(selectedChar.itemList[j]);
        }

        BlockArrow();
    }

    void BlockArrow()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

        if (nowIndex == 0)
        {
            leftArrow.SetActive(false);
        }
        else if(nowIndex == ourList.Count - 1)
        {
            rightArrow.SetActive(false);
        }
    }

    public void MoveVal(int moveVal)
    {
        int dddd = nowIndex + moveVal;

        SetMenu(dddd);
    }
}
