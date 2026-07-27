using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveManager : MonoBehaviour
{
    public static SceneMoveManager Instance;
    public string goingSceneName;

    Camera mainCamera;
    [SerializeField] Canvas can;
    [SerializeField] CanvasGroup fade;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mainCamera = MainCamera.Instance.cam;
        can.worldCamera = mainCamera;
    }

    public void FadeSceneLoad(string sceneName, float time)
    {
        StartCoroutine(FadeLoad(LoadScene, time));
        goingSceneName = sceneName;
    }

    public IEnumerator FadeLoad(Action action, float time)
    {
        UIMovement.DOFade(fade, 1f, time);

        yield return new WaitForSeconds(time);

        action?.Invoke();
        UIMovement.DOFade(fade, 0f, time);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Loading");
    }
}
