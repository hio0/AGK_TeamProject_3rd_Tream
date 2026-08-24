using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEmotion : MonoBehaviour
{
    Character mychar;

    public int imotionStack;
    public int timeLimit;

    public Action<int> OnImotionChanged;

    // Start is called before the first frame update
    void Awake()
    {
        mychar = GetComponent<Character>();
        mychar.characterEmotion = this;

        mychar.OnActingStart += EmotionTrigger;
        mychar.OnDamaged += EmotionTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EmotionTrigger()
    {
        int r = UnityEngine.Random.Range(10, 35);
        int ran = UnityEngine.Random.Range(1, 101);

        if(ran <= r)
        {
            EmotionPlus();
        }
    }

    void EmotionPlus()
    {

    }
}
