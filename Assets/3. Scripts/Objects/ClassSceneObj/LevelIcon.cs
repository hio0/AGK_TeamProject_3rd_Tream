using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelIcon : MonoBehaviour
{
    Character mychar;
    [SerializeField] TMP_Text levelT;

    public void Initialize(Character character)
    {
        mychar = character;
    }

    void Start()
    {

    }

    private void OnDisable()
    {
        mychar.OnLevelUp -= SetLevel;
    }

    void SetLevel()
    {
        levelT.text = mychar.levelCount.ToString();
    }
}
