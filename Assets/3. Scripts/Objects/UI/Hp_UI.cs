using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_UI : MonoBehaviour
{
    Character myChar;

    // Start is called before the first frame update
    void Start()
    {
        myChar = GetComponentInParent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
