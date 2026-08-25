using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StudentInfo : MonoBehaviour
{
    public Character mychar { get; private set; }

    [SerializeField] Image icon;
    [SerializeField] LevelIcon levelIcon;
    [SerializeField] TMP_Text characterNameT;
    [SerializeField] TMP_Text LevelT;
    [SerializeField] TMP_Text hpT;
    [SerializeField] TMP_Text speedT;
    [SerializeField] TMP_Text emotionT;

    public EventTrigger trigger;

    BasicIcon basicIcon;
    public BasicIcon pre_basicIcon;
    [SerializeField] Transform rect;

    public void Initialize(Character character, Transform pa)
    {
        mychar = character;
        rect = pa;
    }

    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = mychar.characterData.iconImage;
        icon.SetNativeSize();

        characterNameT.text = mychar.characterData.defaultCharacterName;
        LevelT.text = $"{mychar.level} / {mychar.maxLevel}";
        hpT.text = $"{mychar.hp} / {mychar.maxHp}";
        speedT.text = $"{mychar.characterData.defaultMinSpeed} / {mychar.characterData.defaultMaxSpeed}";
        if(mychar.characterEmotion != null)
        {
            CharacterEmotion emotion = mychar.characterEmotion;
            emotionT.text = $"{emotion.imotionStack} / 4";
        }
        else
        {
            emotionT.text = "0 / 0";
        }

        Templet.AddEvent(trigger, EventTriggerType.BeginDrag, OnBeginDrag);
        Templet.AddEvent(trigger, EventTriggerType.Drag, OnDrag);
        Templet.AddEvent(trigger, EventTriggerType.EndDrag, OnEndDrag);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        basicIcon = Instantiate(pre_basicIcon, rect);

        basicIcon.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIcon.spriteImage.sprite = mychar.characterData.iconImage;

        basicIcon.can.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveWithMouse(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(basicIcon.gameObject);
        basicIcon = null;
    }

    void MoveWithMouse(PointerEventData eventData)
    {
        RectTransform parentRect = basicIcon.transform.parent as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos
        );

        basicIcon.GetComponent<RectTransform>().anchoredPosition = localPos;
    }
}
