using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorChange : MonoBehaviour
{
    public static ElevatorChange instance;

    [SerializeField] RectTransform rect1;
    [SerializeField] RectTransform rect2;
    [SerializeField] CanvasGroup can1;
    [SerializeField] CanvasGroup can2;

    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float speed;
    bool isIn;

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

    public void Move()
    {
        if (isIn)
        {
            UIMovement.DoSizeMove(rect1, closePos, speed);
            UIMovement.DoSizeMove(rect2, closePos, speed);
        }
        else
        {
            UIMovement.DoSizeMove(rect1, openPos, speed);
            UIMovement.DoSizeMove(rect2, openPos, speed);
        }

        isIn = !isIn;
    }

    public void Canvas(int alpha)
    {
        can1.alpha = alpha;
        can2.alpha = alpha;
    }
}
