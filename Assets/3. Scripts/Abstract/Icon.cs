using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconContext
{
    public IconData data;
    public Character target;
    public Skill skill;
}

[Serializable]
public abstract class Icon
{
    public IconData data;
    public Character target;
    public Skill skill;
    public int stack;
    public int remainTime;

    public void Initialize(IconContext context)
    {
        data = context.data;
        target = context.target;
        skill = context.skill;

        stack = 1;
        remainTime = data.limitTurn;
    }

    public void AddStack(int stack)
    {
        int plused = this.stack + stack;

        if (plused > data.limitStack)
        {
            plused = data.limitStack;
        }

        this.stack = plused;
        remainTime = data.limitTurn;
    }

    /*
    public void IsRemoveIcon()
    {
        remainTime--;

        if(remainTime <= 0)
        {
            user.RemoveIcon(this);
        }
    }
    */
}
