using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class UIMovement
{
    // Made'n
    public static IEnumerator SizeSetAnimation(RectTransform what, Vector2 target, float speed)
    {
        while ((what.sizeDelta - target).sqrMagnitude > 0.001f)
        {
            float x = Mathf.Lerp(what.sizeDelta.x, target.x, Time.deltaTime * speed);
            float y = Mathf.Lerp(what.sizeDelta.y, target.y, Time.deltaTime * speed);

            what.sizeDelta = new Vector2(x, y);
            yield return null;
        }
    }

    // DOTWeen
    public static void DoAnchorMove(RectTransform rect, Vector2 targetPos, float time)
    {
        rect.DOAnchorPos(targetPos, time);
    }

    public static void DoSizeMove(RectTransform rect, Vector2 targetSize, float time)
    {
        rect.DOSizeDelta(targetSize, time);
    }

    public static void DoRotation(RectTransform rect, Vector3 spinPos, float time)
    {
        rect.DORotate(spinPos, time);
    }

    public static void DOFade(CanvasGroup what, float howmuch, float time)
    {
        what.DOFade(howmuch, time);
    }
}
