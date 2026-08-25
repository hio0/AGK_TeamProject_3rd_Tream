using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionCutScene : MonoBehaviour
{
    [SerializeField] Image cutSceneBg;
    [SerializeField] Image cutSceneImage;
    [SerializeField] TMP_Text text;

    [SerializeField] RectTransform rect;
    [SerializeField] Vector2 targetSize;
    [SerializeField] float speed;

    bool isPokju;

    public void Initialize(bool isPockju, Sprite sprite, Color32 col)
    {
        this.isPokju = isPockju;
        cutSceneImage.sprite = sprite;
        cutSceneImage.SetNativeSize();
        cutSceneBg.color = col;

        rect.sizeDelta = new Vector2(targetSize.x, 0);
        text.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        Act();
    }
    
    void Act()
    {
        IEnumerator Cor()
        {
            yield return new WaitForSeconds(1.5f);

            AudioSource soure = SoundManager.instance.GetSoundData.Invoke().bgm;
            soure.volume = 0.05f;

            UIMovement.DoSizeMove(rect, targetSize, speed);

            yield return new WaitForSeconds(speed + 0.5f);

            string massage = null;
            if(isPokju)
            {
                massage = "감정<color=#E3762E>폭주</color>";
            }
            else
            {
                massage = "감정표출";
            }
            StartCoroutine(UIMovement.Typing(text, massage, 0.3f));

            yield return new WaitForSeconds(2.5f);

            soure.volume = 0.2f;
            Destroy(gameObject);
        }

        StartCoroutine(Cor());
    }
}
