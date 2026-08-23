using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] RectTransform animationObject;
    [SerializeField] float spinSpeed;
    string goingSceneName;

    [SerializeField] GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        Loading();
    }

    void Loading()
    {
        goingSceneName = SceneMovement.goingSceneName;

        switch (goingSceneName)
        {
            case "School":
                main.SetActive(true);
                break;
        }

        IEnumerator LoadScene()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(goingSceneName);
            op.allowSceneActivation = false;

            float progress = 0f;
        
            while (!op.isDone)
            {
                float target = Mathf.Clamp01(op.progress / 0.9f);

                progress = Mathf.Lerp(progress, target, Time.deltaTime * 5f);

                if (progress >= 0.99f)
                {
                    op.allowSceneActivation = true;
                }

                yield return null;
            }
        }
        StartCoroutine(LoadScene()); 
    }
}
