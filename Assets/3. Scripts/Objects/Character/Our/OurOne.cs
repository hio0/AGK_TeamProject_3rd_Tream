using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurOne : Character
{
    public OurOne()
    {
        characterName = "요이";
        hp = 10;
        minSpeed = 3;
    }

    private void Start()
    {
        skillList.Add(new Slash());
    }
}
