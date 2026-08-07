using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StudentInfo : MonoBehaviour
{
    public Character mychar;
    public Image icon;

    public TMP_Text characterNameT;
    public TMP_Text LevelT;
    public TMP_Text hpT;
    public TMP_Text stmT;

    public Image hpFillImage;
    public Image stmFillImage;

    public GameObject pre_artifectIcon;
    public Transform parent_acrtifectIcon;

    public EventTrigger trigger;
    Character dragObject;
    BasicIcon basicIcon;
    public BasicIcon pre_basicIcon;

    public void Initialize(Character character)
    {
        mychar = character;
    }

    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = mychar.characterData.iconImage;

        characterNameT.text = mychar.characterData.defaultCharacterName;
        LevelT.text = $"Lv.<size=35>{mychar.characterData.nowLevel}</size>";
        hpT.text = $"{mychar.characterData.nowHp} / {mychar.characterData.nowMaxHp}";

        hpFillImage.fillAmount = (float)mychar.characterData.nowHp / (float)mychar.characterData.nowMaxHp;

        Templet.AddEvent(trigger, EventTriggerType.BeginDrag, OnBeginDrag);
        Templet.AddEvent(trigger, EventTriggerType.Drag, OnDrag);
        Templet.AddEvent(trigger, EventTriggerType.EndDrag, OnEndDrag);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragObject = eventData.pointerDrag.GetComponent<StudentInfo>().mychar;

        RectTransform rect = AgitManager.instance.GetUIRect?.Invoke();
        basicIcon = Instantiate(pre_basicIcon, rect);
        MoveWithMouse(eventData);

        BasicIconData basicIconData = basicIcon.ReturnImage();

        basicIconData.spriteImage.color = new Color32(255, 255, 255, 255);
        basicIconData.spriteImage.sprite = mychar.characterData.iconImage;

        basicIconData.canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveWithMouse(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragObject = null;

        Destroy(basicIcon.gameObject);
        basicIcon = null;
    }

    void MoveWithMouse(PointerEventData eventData)
    {
        RectTransform rect = AgitManager.instance.GetUIRect?.Invoke();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos
        );

        basicIcon.GetComponent<RectTransform>().anchoredPosition = localPos;
    }
}
