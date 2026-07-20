using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    public Skill myskill;

    public TMP_Text skillNameT;
    public TMP_Text skillBlaBlaT;

    public void Initialize(Skill skill)
    {
        myskill = skill;
    }

    // Start is called before the first frame update
    void Start()
    {
        skillNameT.text = myskill.skillName;
        //skillBlaBlaT.text = 
    }
}
