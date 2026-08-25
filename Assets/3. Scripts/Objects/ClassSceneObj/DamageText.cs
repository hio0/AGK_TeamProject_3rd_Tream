using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] CanvasGroup can;
    [SerializeField] Vector2 targetSize;

    [SerializeField] TMP_Text text;

    public void Initialize(string text, Color32 mainCol, Vector2 rect)
    {
        this.text.text = text;
        this.text.color = mainCol;
        this.rect.anchoredPosition = rect;
    }

    // Start is called before the first frame update
    void Start()
    {
        Act();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Act()
    {
        IEnumerator Cor()
        {
            can.alpha = 1f;

            yield return new WaitForSeconds(1f);
            UIMovement.DOFade(can, 0f, 0.5f);

            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }

        StartCoroutine(Cor());
    }
}
