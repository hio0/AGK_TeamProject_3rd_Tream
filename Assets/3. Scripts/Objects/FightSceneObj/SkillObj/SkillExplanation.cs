using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class SkillExplanation : MonoBehaviour
{
    SkillData myskill;

    [SerializeField] TMP_Text skillNameT;
    [SerializeField] ScrollView skillExplanationScroll;
    [SerializeField] TMP_Text skillExplanationT;

    RectTransform rect;
    [SerializeField] Vector2 targetPos = new Vector2(-700, -415);

    public void Initialize(SkillData skill)
    {
        myskill = skill;
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        /*
        skillNameT.text = myskill.skillName;
        skillExplanationT.text = myskill.skillExplanation;

        UIMovement.DoAnchorMove(rect, targetPos, 0.4f);
        */
    }

    private void OnDisable()
    {
        skillNameT.text = null;
        skillExplanationT.text = null;

        rect.anchoredPosition = new Vector2(-700, -700);
    }
}
