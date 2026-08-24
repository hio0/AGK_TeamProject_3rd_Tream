using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmotionIcon : MonoBehaviour
{
    Character mychar;
    CharacterEmotion emotion;
    [SerializeField] TMP_Text emoT;
    bool isour;

    void Start()
    {
        mychar = GetComponentInParent<Character>();
        if(!mychar.iOurUnit)
        {
            isour = true;
            gameObject.SetActive(false);
        }
        else
        {
            emotion = GetComponentInParent<CharacterEmotion>();

            emotion.OnImotionChanged += SetEmotion;

            SetEmotion(0);
        }
    }

    private void OnDisable()
    {
        if(!isour)
        {
            emotion.OnImotionChanged -= SetEmotion;
        }
    }

    void SetEmotion(int a)
    {
        emoT.text = emotion.imotionStack.ToString();
    }
}
