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

    // Start is called before the first frame update
    void Start()
    {
        Loading();
    }

    void Loading()
    {
        goingSceneName = SceneMoveManager.Instance.goingSceneName;

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
                    yield return new WaitForSeconds(1.5f); // 연출용 딜레이
                    Action action = () => op.allowSceneActivation = true;

                    StartCoroutine(SceneMoveManager.Instance.FadeLoad(action, 1f));
                }

                yield return null;
            }
        }
        StartCoroutine(LoadScene()); 
    }
}
