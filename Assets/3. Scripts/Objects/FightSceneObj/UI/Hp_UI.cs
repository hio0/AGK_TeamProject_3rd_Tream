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
    [SerializeField] Image hpFillBg;

    // Start is called before the first frame update
    void Start()
    {
        can.alpha = 1f;
        myChar = GetComponentInParent<Character>();

        SetValue();

        myChar.OnDamaged -= Damaged;
        myChar.OnDamaged += Damaged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Damaged()
    {
        SetValue();

        IEnumerator HpBgFill()
        {
            yield return new WaitForSeconds(0.5f);

            while (true)
            {
                hpFillBg.fillAmount -= 0.05f;

                if (hpFillBg.fillAmount <= hpFillImage.fillAmount)
                {
                    hpFillBg.fillAmount = hpFillImage.fillAmount;
                    break;
                }

                yield return null;
            }
        }

        StartCoroutine(HpBgFill());
    }

    void SetValue()
    {
        hpText.text = $"{myChar.hp} / {myChar.maxHp}";
        hpFillImage.fillAmount = (float)myChar.hp / (float)myChar.maxHp;
    }
}
