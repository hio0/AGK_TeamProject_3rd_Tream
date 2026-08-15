using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class UIMovement
{
    // Made'n
    public static IEnumerator MoveAnimation(RectTransform what, Vector2 target, float speed)
    {
        while ((what.anchoredPosition - target).sqrMagnitude > 0.001f)
        {
            float x = Mathf.Lerp(what.anchoredPosition.x, target.x, Time.deltaTime * speed);
            float y = Mathf.Lerp(what.anchoredPosition.y, target.y, Time.deltaTime * speed);

            what.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
    }

    public static IEnumerator LerpFade(RectTransform what, CanvasGroup can, Vector2 target, float speed)
    {
        while (can.alpha > 0.01f)
        {
            float distance = Vector2.Distance(what.anchoredPosition, target);
            float maxDistance = target.y - what.anchoredPosition.y;
            float t = 1f - Mathf.Clamp01(distance / maxDistance);

            t *= t;

            can.alpha = Mathf.Lerp(can.alpha, 0f, t * speed * Time.deltaTime);

            yield return null;
        }

        can.alpha = 0f;
    }

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
