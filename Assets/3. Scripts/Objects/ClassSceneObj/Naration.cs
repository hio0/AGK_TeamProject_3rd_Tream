using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Naration : MonoBehaviour
{
    [SerializeField] TMP_Text narationT;
    [SerializeField] RectTransform rect;
    [SerializeField] CanvasGroup can;

    public void Initialize(string naration)
    {
        narationT.text = naration;
    }

    // Start is called before the first frame update
    void Start()
    {
        Animatied();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Animatied()
    {
        can.alpha = 1f;

        IEnumerator Cor()
        {
            UIMovement.DoAnchorMove(rect, Vector2.zero, 0.5f);

            yield return new WaitForSeconds(5f);

            UIMovement.DOFade(can, 0, 1f);
        }

        StartCoroutine(Cor());
    }
}
