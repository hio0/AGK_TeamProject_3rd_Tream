using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text changeT;
    [SerializeField] CanvasGroup can;

    // Start is called before the first frame update
    void Start()
    {
        SchoolManager.instance.OnMoneyChanged += ChangedMoney;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnMoneyChanged -= ChangedMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangedMoney(int value)
    {
        can.DOKill();
        StopAllCoroutines();

        IEnumerator Cor()
        {
            can.alpha = 1;
            changeT.text = value.ToString("+#;-#;0");

            yield return new WaitForSeconds(1f);

            UIMovement.DOFade(can, 0f, 2f);
        }

        StartCoroutine(Cor());
    }
}
