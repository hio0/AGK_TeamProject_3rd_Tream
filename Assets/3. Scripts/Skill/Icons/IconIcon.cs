using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconIcon : MonoBehaviour
{
    Icon myIcon;
    Character myChar;

    public Image iconIcon;
    public TMP_Text iconStackT;

    public void Initialize(Icon icon,  Character character)
    {
        myIcon = icon;
        myChar = character;
    }

    private void Start()
    {
        myChar.OnIconStackChange += AddStackT;
    }

    void AddStackT(Icon icon)
    {
        if(myIcon != icon)
        {
            return;
        }

        iconStackT.text = myIcon.stack.ToString();
    }
}
