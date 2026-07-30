using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Range : MonoBehaviour
{
    public bool isOur;

    public List<Character> GetCharacter()
    {
        List<Character> list = new List<Character>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Character character = transform.GetChild(i).GetComponent<Character>();

            list.Add(character);
            if(isOur)
            {
                character.iOurUnit = true;
            }
        }

        return list;
    }
}
