using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static Action OnNoticedSomething;

    public static void ClearAllEvent()
    {
        OnNoticedSomething = null;
    }
}
