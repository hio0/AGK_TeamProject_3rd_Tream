using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconText : MonoBehaviour
{
    public Image iconImage;
    Sprite iconSprie;
    public TMP_Text iconText;
    string text;
    Color32 color;
    Vector2 targetPos;

    public float speed;

    public CanvasGroup can;
    public RectTransform rect;

    public void Initialize(IconData data)
    {
        iconSprie = data.iconImage;
        text = data.iconName;
        color = data.textColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        iconImage.sprite = iconSprie;
        iconImage.color = color;
        iconText.text = text;
        iconText.color = color;

        targetPos = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + 140);
        Animation();
    }

    void Animation()
    {
        can.alpha = 1f;

        IEnumerator Cor()
        {
            UIMovement.DoAnchorMove(rect, targetPos, speed);
            StartCoroutine(UIMovement.LerpFade(rect, can, targetPos));

            yield return new WaitForSeconds(speed + 0.1f);

            Destroy(gameObject);
        }

        StartCoroutine(Cor());
    }
}
