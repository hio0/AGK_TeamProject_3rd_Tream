using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpLetterBox_UI : LetterBox
{
    public static UpLetterBox_UI Instance;

    protected override void SetStatic()
    {
        Instance = this;
    }
}
