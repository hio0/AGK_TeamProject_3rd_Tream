using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class SkillExplanation : MonoBehaviour
{
    [SerializeField] TMP_Text skillNameT;
    [SerializeField] TMP_Text skillExplanationT;
    [SerializeField] TMP_Text skillTypeT;

    public void Initialize(SkillData skill, TMP_Text text)
    {
        skillNameT.text = skill.skillName;
        skillExplanationT.text = skill.skillExplanation;
        skillTypeT = text;
    }

    private void OnDisable()
    {
        skillNameT.text = null;
        skillExplanationT.text = null;
        skillTypeT = null;
    }
}
