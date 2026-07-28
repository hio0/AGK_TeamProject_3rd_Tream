using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SkillEffect
{
    public abstract void Effect(SkillContext skillContext);
}
