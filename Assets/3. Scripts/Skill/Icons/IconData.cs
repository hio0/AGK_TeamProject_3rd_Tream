using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IconData")]
public class IconData : ScriptableObject
{
    [SerializeReference, SubclassSelector] public readonly Icon myIcon;
    public string iconName;
    public Sprite iconImage;

    public int limitStack;
    public int limitTurn;

    public Color32 textColor;

    [TextArea] public string iconExplanation;
}
