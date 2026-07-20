using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurOne : Character
{
    private void Start()
    {
        skillList.Add(new Slash());
    }
}
