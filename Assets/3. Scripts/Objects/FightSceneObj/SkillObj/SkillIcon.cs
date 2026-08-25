using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    SkillData myskill;
    SkillExplanation skillExplanation;
    Character user;

    [SerializeField] Outline mainOutline;
    [SerializeField] Outline subOutline;
    [SerializeField] TMP_Text text;
    public EventTrigger trigger;

    static Action OnClicked;

    public void Initialize(SkillData skill, SkillExplanation skillExplan, Character user) // 사실 이렇게 하기 보단 이벤트로 값 넘겨 받는게 맞긴 하다 ㅇㅇ,,,
    {
        myskill = skill;
        skillExplanation = skillExplan;
        this.user = user;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetIcon();

        Templet.AddEvent(trigger, EventTriggerType.PointerClick, OnClick);
        Templet.AddEvent(trigger, EventTriggerType.PointerEnter, OnEnter);
        Templet.AddEvent(trigger, EventTriggerType.PointerExit, OnExit);

        OnClicked += DeleteEvent;
    }

    private void OnDisable()
    {
        OnClicked -= DeleteEvent;
    }

    void SetIcon()
    {
        Color32 color = new Color32();
        string name = null;

        switch(myskill.skillType)
        {
            case SkillData.actType.attack:
                color = new Color32(174, 104, 104, 255);
                name = "공격";
                break;
            case SkillData.actType.guard:
                color = new Color32(113, 120, 195, 255);
                name = "수비";
                break;
            case SkillData.actType.special:
                color = new Color32(113, 195, 134, 255);
                name = "특수";
                break;
            case SkillData.actType.emotion:
                color = new Color32(195, 179, 113, 255);
                name = "감정";
                break;
        }

        mainOutline.effectColor = color;
        subOutline.effectColor = color;
        text.color = color;
        text.text = name;
    }

    SkillData ReturnSkill()
    {
        return myskill;
    }

    void DeleteEvent()
    {
        FightManager.Instance.GetNowSkill -= ReturnSkill;
    }

    void SkillExplanation()
    {
        skillExplanation.Initialize(myskill, text);
        skillExplanation.gameObject.SetActive(true);
    }

    void OnClick(PointerEventData data)
    {
        SkillExplanation();
        OnClicked?.Invoke();

        FightManager.Instance.GetNowSkill += ReturnSkill;
        FightManager.Instance.OnTargetFinding?.Invoke();

        SchoolManager.instance.OnNoticedSomething("타겟을 정하자!");
        FocusCamera.Instance.Live(0);
    }

    void OnEnter(PointerEventData data)
    {
        SkillExplanation();
    }

    void OnExit(PointerEventData data)
    {
        skillExplanation.gameObject.SetActive(false);
    }
}
