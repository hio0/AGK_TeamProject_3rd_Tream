using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public static SceneChange instance;

    [SerializeField] RectTransform rect;

    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float speed;

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

    public void Move(bool isIn)
    {
        if (isIn)
        {
            IEnumerator Cor()
            {
                rect.anchoredPosition = openPos;
                UIMovement.DoAnchorMove(rect, Vector2.zero, speed);

                yield return new WaitForSeconds(1);

                UIMovement.DoAnchorMove(rect, closePos, speed);
            }

            StartCoroutine(Cor());
        }
        else
        {
            IEnumerator Cor()
            {
                rect.anchoredPosition = closePos;
                UIMovement.DoAnchorMove(rect, Vector2.zero, speed);

                yield return new WaitForSeconds(1f);

                UIMovement.DoAnchorMove(rect, openPos, speed);
            }

            StartCoroutine(Cor());
        }

        isIn = !isIn;
    }
}
