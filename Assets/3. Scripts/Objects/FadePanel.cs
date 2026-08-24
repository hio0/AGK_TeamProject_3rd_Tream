using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    public static FadePanel instance;
    [SerializeField] CanvasGroup can;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Canvas(int index, float speed)
    {
        Debug.Log(can.alpha);
        can.DOKill();
        can.alpha = 1;
        UIMovement.DOFade(can, index, speed);
    }
}
