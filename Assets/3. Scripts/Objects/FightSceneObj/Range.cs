using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
            else
            {
                character.iOurUnit = false;
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
