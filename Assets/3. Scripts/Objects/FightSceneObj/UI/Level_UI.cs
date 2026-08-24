using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level_UI : MonoBehaviour
{
    Character myChar;

    [SerializeField] Image levelFillImage; // iIIiIiIIi
    [SerializeField] Image levelFillBg;

    // Start is called before the first frame update
    void Start()
    {
        myChar = GetComponentInParent<Character>();
        if(!myChar.iOurUnit)
        {
            gameObject.SetActive(false);
        }

        SetValue(0);

        myChar.OnLevelChanged -= SetValue;
        myChar.OnLevelChanged += SetValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        myChar.OnLevelChanged -= SetValue;
    }

    void SetValue(int xp)
    {
        levelFillImage.fillAmount = (float)myChar.level / (float)myChar.maxLevel;
    }
}
