using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
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

    public static IEnumerator LerpFade(RectTransform what, CanvasGroup can, Vector2 target)
    {
        Vector2 startPos = what.anchoredPosition;
        float totalDistance = Vector2.Distance(startPos, target);

        if (totalDistance <= 0f)
        {
            can.alpha = 0f;
            yield break;
        }

        while (true)
        {
            float currentDistance = Vector2.Distance(startPos, what.anchoredPosition);

            float progress = currentDistance / totalDistance;

            can.alpha = 1f - Mathf.Clamp01(progress);

            if (progress >= 1f)
                break;

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

    public static IEnumerator Typing(TMP_Text text, string message, float duration)
    {
        text.text = message;
        text.maxVisibleCharacters = 0;

        for (int i = 0; i <= message.Length; i++)
        {
            text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(duration);
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
