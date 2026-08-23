using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgitManager : MonoBehaviour
{
    public static AgitManager instance;

    public List<Character> dedugs;

    [SerializeField] float loadSceneTime;

    private void Awake()
    {
        instance = this;
        ImportantData.canUsedStudents = dedugs;
    }

    public void Attendance()
    {
        //SceneMoveManager.Instance.FadeSceneLoad("School", loadSceneTime);
    }
}
