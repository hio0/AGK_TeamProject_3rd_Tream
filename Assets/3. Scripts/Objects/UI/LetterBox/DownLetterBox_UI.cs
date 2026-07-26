using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLetterBox_UI : LetterBox
{
    // ! 버그 !
    // FightManager의 Awake보다 이 스크립트의 OnEnable / Awake가 먼저 호출되는 말도 안되는 버그 발생.
    // Project Setting에서 Order값을 변경해 땜빵해놓음. 발생 원인은 불명.

    private void OnEnable()
    {
        deffultTargetingPos = new Vector2(0, 240f);
        animationSpeed = 3f;

        FightManager.Instance.OnFightStart += Move;
    }
    private void OnDisable()
    {
        FightManager.Instance.OnFightStart -= Move;
    }
}
