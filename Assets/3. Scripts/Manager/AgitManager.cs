using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgitManager : MonoBehaviour
{
    [SerializeField] float loadSceneTime; 

    public void Attendance()
    {
        SceneMoveManager.Instance.FadeSceneLoad("School", loadSceneTime);
    }
}
