using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : ActionObject
{
    Character myChar;

    [SerializeField] TMP_Text hpText;
    [SerializeField] Image hpFillImage; // iIIiIiIIi

    // Start is called before the first frame update
    void Start()
    {
        can.alpha = 1f;
        myChar = GetComponentInParent<Character>();

        SetValue();

        myChar.OnDamaged -= SetValue;
        myChar.OnDamaged += SetValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetValue()
    {
        hpText.text = $"{myChar.hp} / {myChar.maxHp}";
        hpFillImage.fillAmount = (float)myChar.hp / (float)myChar.maxHp;
    }
}
