using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentMenu : MonoBehaviour
{
    public StudentInfo pre_studentInfo;
    public Transform parent_studentInfo;

    // Start is called before the first frame update
    void Start()
    {
        foreach(CharacterData characterData in ImportantData.canUsedStudents)
        {
            StudentInfo stuInfo = Instantiate(pre_studentInfo, parent_studentInfo);
            stuInfo.Initialize(characterData);
        }
    }
}
