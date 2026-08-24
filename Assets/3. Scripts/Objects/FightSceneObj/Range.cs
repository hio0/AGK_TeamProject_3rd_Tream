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
                    character.AddComponent<CharacterEmotion>();
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

    public void Clear()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Exp()
    {
        IEnumerator Cor()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Character>().GetExp(Random.Range(ImportantData.dayCount * 5, ImportantData.dayCount * 20));

                yield return new WaitForSeconds(1f);
            }
        }

        StartCoroutine(Cor());
    }
}
