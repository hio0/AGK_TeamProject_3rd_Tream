using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Skill, IAttackSkill
{
    public int damage { get; set; }

    public Slash()
    {
        skillName = "베기";
        damage = 5;
    }

    public override void Effect(Character user, Character target)
    {

    }
}
