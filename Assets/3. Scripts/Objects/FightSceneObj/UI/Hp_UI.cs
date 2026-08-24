using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : ActionObject
{
    Character myChar;

    [SerializeField] TMP_Text hpText;
    [SerializeField] Image hpFillImage; // iIIiIiIIi
    [SerializeField] Image hpHealFill;
    [SerializeField] Image hpFillBg;

    [SerializeField] DamageText pre_text;

    int nowHp;

    // Start is called before the first frame update
    void Start()
    {
        can.alpha = 1f;
        myChar = GetComponentInParent<Character>();

        SetValue();

        myChar.OnDamaged -= Damaged;
        myChar.OnDamaged += Damaged;

        myChar.OnHeal += Heal; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        myChar.OnDamaged -= Damaged;
        myChar.OnHeal -= Heal;
    }

    void Damaged()
    {
        IEnumerator HpBgFill()
        {
            int hp = nowHp;
            SetValue();

            hpFillBg.gameObject.SetActive(true);

            Vector2 parent_text = new();
            if (myChar.iOurUnit)
            {
                parent_text = new Vector2(-129.3f, 328.8f);
            }
            else
            {
                parent_text = new Vector2(129.3f, 328.8f);
            }
            RectTransform charRect = myChar.GetComponent<RectTransform>();
            RectTransform uiRect = GameManager.instance.GetUIRect.Invoke();

            DamageText text = Instantiate(pre_text, uiRect);

            RectTransform textRect = text.GetComponent<RectTransform>();

            textRect.position = charRect.TransformPoint(parent_text);

            int a = hp - nowHp;
            text.Initialize(a.ToString(), new Color32(243, 77, 103, 255), parent_text);

            yield return new WaitForSeconds(0.5f);

            while (true)
            {
                hpFillBg.fillAmount -= 0.03f;

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

    void Heal(int healing)
    {
        IEnumerator HpBgFill()
        {
            int hp = nowHp;
            SetValue();
            float fill = (float)healing / myChar.maxHp;

            hpHealFill.gameObject.SetActive(true);

            Vector2 parent_text = new();
            if (myChar.iOurUnit)
            {
                parent_text = new Vector2(-129.3f, 328.8f);
            }
            else
            {
                parent_text = new Vector2(129.3f, 328.8f);
            }
            RectTransform charRect = myChar.GetComponent<RectTransform>();
            RectTransform uiRect = GameManager.instance.GetUIRect.Invoke();

            DamageText text = Instantiate(pre_text, uiRect);

            RectTransform textRect = text.GetComponent<RectTransform>();

            textRect.position = charRect.TransformPoint(parent_text);

            int a = hp - nowHp;
            text.Initialize(a.ToString(), new Color32(78, 243, 99, 255), parent_text);

            yield return new WaitForSeconds(0.5f);

            while (true)
            {
                hpHealFill.fillAmount += 0.03f;

                if (hpHealFill.fillAmount <= fill)
                {
                    hpHealFill.fillAmount = fill;
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
        nowHp = myChar.hp;

        hpHealFill.gameObject.SetActive(false);
        hpFillBg.gameObject.SetActive(false);
    }
}
