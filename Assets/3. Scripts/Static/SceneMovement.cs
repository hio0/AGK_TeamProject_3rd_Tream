using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneMovement
{
    public static string goingSceneName;

    public static void LoadLoadingScene(string sceneName)
    {
        goingSceneName = sceneName;
        SceneManager.LoadScene("Loading");
    }
}
