using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Range : MonoBehaviour
{
    public bool isOur;

    public List<Character> GetCharacter()
    {
        List<Character> list = new List<Character>();

        if(transform.childCount == 0)
        {
            return null;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Character character = transform.GetChild(i).GetComponent<Character>();

            list.Add(character);
            if (isOur)
            {
                character.iOurUnit = true;
                character.TryGetComponent<OurCharacter>(out OurCharacter our);
                if (our == null)
                {
                    character.AddComponent<OurCharacter>();
                }
            }
            else
            {
                character.iOurUnit = false;
                character.TryGetComponent<EnemyCharacter>(out EnemyCharacter ene);
                if (ene == null)
                {
                    character.AddComponent<EnemyCharacter>();
                }
            }
        }

        return list;
    }
}
