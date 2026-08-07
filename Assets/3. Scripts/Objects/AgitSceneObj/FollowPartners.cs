using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPartners : MonoBehaviour
{
    public List<StudentIcon> icons;
    public GameObject goButton;

    // Start is called before the first frame update
    void Start()
    {
        AgitManager.instance.OnDroped += SetStart;

        SetStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetStart()
    {
        bool isOk = true;
        foreach (StudentIcon icon in icons)
        {
            if(icon.dragObject == null)
            {
                isOk = false;
            }
        }

        if(isOk)
        {
            goButton.SetActive(true);

            goButton.GetComponent<CanvasGroup>().alpha = 0;
            UIMovement.DOFade(goButton.GetComponent<CanvasGroup>(), 1f, 1f);
        }
        else
        {
            goButton.SetActive(false);
        }
    }

    public void Started()
    {
        foreach (StudentIcon icon in icons)
        {
            ImportantData.usedStudents.Add(icon.dragObject);
        }
    }
}
