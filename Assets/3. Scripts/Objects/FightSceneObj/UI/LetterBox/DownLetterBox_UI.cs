using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLetterBox_UI : LetterBox
{
    public static DownLetterBox_UI Instance;

    protected override void SetStatic()
    {
        Instance = this;
    }
}
