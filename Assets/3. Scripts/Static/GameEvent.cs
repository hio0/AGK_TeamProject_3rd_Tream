using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static Action<string> OnNoticedSomething; // 나레이션할만한 행동 일어남

    public static void ClearAllEvent()
    {
        OnNoticedSomething = null;
    }
}
