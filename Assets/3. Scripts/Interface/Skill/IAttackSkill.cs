using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackSkill
{
    int MinDamage { get; }
    int MaxDamage { get; }

    event Action OnAttack;
}
