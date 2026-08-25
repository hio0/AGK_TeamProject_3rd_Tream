using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEmotion : MonoBehaviour
{
    Character mychar;

    public int imotionStack;
    bool limitStart;

    public Action OnImotionChanged;

    // Start is called before the first frame update
    void Awake()
    {
        mychar = GetComponent<Character>();
        mychar.characterEmotion = this;

        mychar.OnActingStart += EmotionTrigger;
        mychar.OnDamaged += EmotionTrigger;

        mychar.OnActingStart += LimitStart;
        FightManager.Instance.OnActingFinished += PokjuEnd;
    }

    private void OnDisable()
    {
        mychar.OnActingStart -= EmotionTrigger;
        mychar.OnDamaged -= EmotionTrigger;

        mychar.OnActingStart -= LimitStart;
        FightManager.Instance.OnActingFinished -= PokjuEnd;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EmotionTrigger()
    {
        int r = UnityEngine.Random.Range(10, 35);
        int ran = UnityEngine.Random.Range(1, 101);

        if (ran <= r)
        {
            EmotionPlus();
        }
    }

    void EmotionPlus()
    {
        imotionStack++;
        if (imotionStack > 4)
        {
            imotionStack = 4;
            limitStart = true;
        }

        SchoolManager.instance.Speak(mychar.characterData.speakData, "emotionUp", this.transform);
        mychar.EmotionUp();
        OnImotionChanged.Invoke();
    }

    public void EmotionMinus()
    {
        imotionStack = 0;
    }

    void LimitStart()
    {
        if(!limitStart)
        {
            return;
        }
        else
        {
            int r = UnityEngine.Random.Range(1, 101);
            int limit = UnityEngine.Random.Range(10, 50);
            SchoolManager.instance.Speak(mychar.characterData.speakData, "emotionLimit", this.transform);

            if (r <= limit)
            {
                mychar.iPokju = true;
            }
        }
    }

    void PokjuEnd()
    {
        if(mychar.iPokju)
        {
            mychar.iPokju = false;
            limitStart = false;
            EmotionMinus();
        }
    }
}
