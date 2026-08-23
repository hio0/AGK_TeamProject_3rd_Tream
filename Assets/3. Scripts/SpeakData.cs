using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpeakData")]
public class SpeakData : ScriptableObject
{
    [TextArea] public List<string> opinionSpeach_approval;
    [TextArea] public List<string> opinionSpeach_opposite;
}
