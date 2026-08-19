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
        Debug.Log(character);
    }

    void Start()
    {
        if(mychar == null)
        {
            mychar = GetComponentInParent<Character>();
        }

        SetLevel();

        mychar.OnLevelUp += SetLevel;
    }

    private void OnDisable()
    {
        mychar.OnLevelUp -= SetLevel;
    }

    void SetLevel()
    {
        levelT.text = mychar.level.ToString();
    }
}
