using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentMenu : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float speed;
    bool isIn;

    [SerializeField] StudentInfo pre_studentInfo;
    [SerializeField] Transform parent_studentInfo;
    [SerializeField] Transform parent_icon;

    // Start is called before the first frame update
    void OnEnable()
    {
        rect.anchoredPosition = closePos;
        SetMenu();
    }

    public void Move()
    {
        if (isIn)
        {
            UIMovement.DoAnchorMove(rect, closePos, speed);
        }
        else
        {
            UIMovement.DoAnchorMove(rect, openPos, speed);
        }

        isIn = !isIn;
    }

    void SetMenu()
    {
        for (int i = 0; i < parent_studentInfo.childCount; i++)
        {
            Destroy(parent_studentInfo.GetChild(i).gameObject);
        }

        IEnumerator Cor()
        {
            for (int i = 0; i < ImportantData.canUsedStudents.Count; i++)
            {
                StudentInfo info = Instantiate(pre_studentInfo, parent_studentInfo);
                info.Initialize(ImportantData.canUsedStudents[i], parent_icon);

                // 10개 만들 때마다 한 프레임 쉬기
                if (i % 10 == 0)
                    yield return null;
            }
        }

        StartCoroutine(Cor());
    }
}
