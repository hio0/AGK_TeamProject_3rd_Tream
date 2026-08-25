using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject panel;
    public CanvasGroup can;
    public CanvasGroup title;

    public List<RectTransform> buttons;
    public float openPos;

    public AudioClip mainTheme;

    private void Start()
    {
        StartAnimation();
    }

    public void Started()
    {
        panel.SetActive(true);

        AudioSource soure = SoundManager.instance.GetSoundData.Invoke().bgm;
        SoundManager.instance.FadeSound(soure, 0.2f, 1f);
    }

    public void Setting()
    {

    }

    public void Exited()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void StartAnimation()
    {
        panel.SetActive(false);

        can.alpha = 1f;
        title.alpha = 0f;
        UIMovement.DOFade(can, 0, 1f);

        SoundManager.instance.BGMPlay(mainTheme);

        IEnumerator Cor()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                UIMovement.DoAnchorMove(buttons[i], new Vector2(openPos, buttons[i].anchoredPosition.y), 1f);

                yield return new WaitForSeconds(0.5f);
            }

            UIMovement.DOFade(title, 1f, 5f);
        }

        StartCoroutine(Cor());
    }
}
