using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgitManager : MonoBehaviour
{
    public static AgitManager instance;

    public Action<string, GameObject> OnButtonClicked;

    public Func<RectTransform> GetUIRect;
    public CharacterData dedugs;

    [SerializeField] float loadSceneTime;

    private void Awake()
    {
        instance = this;
        ImportantData.canUsedStudents.Add(dedugs);
    }

    public void Attendance()
    {
        SceneMoveManager.Instance.FadeSceneLoad("School", loadSceneTime);
    }
}
