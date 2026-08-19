using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    public ItemData data;
    int count;

    [SerializeField] RectTransform rect;
    [SerializeField] Image image;
    [SerializeField] CanvasGroup can;
    [SerializeField] TMP_Text plusCountT;
    [SerializeField] TMP_Text itemNameT;

    public void Initialize(ItemData data, int added)
    {
        this.data = data;
        count = added;
    }

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = data.itemImage;
        plusCountT.text = count.ToString("+#;-#;0");
        itemNameT.text = data.itemName;

        Animatied();
    }

    void Animatied()
    {
        can.alpha = 1f;

        IEnumerator Cor()
        {
            UIMovement.DoAnchorMove(rect, Vector2.zero, 0.5f);

            yield return new WaitForSeconds(3f);

            UIMovement.DOFade(can, 0, 1f);
        }

        StartCoroutine(Cor());
    }
}
