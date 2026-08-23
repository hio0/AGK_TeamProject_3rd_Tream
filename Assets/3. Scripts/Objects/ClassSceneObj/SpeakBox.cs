using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class SpeakBox : MonoBehaviour
{
    public static SpeakBox Instance;

    [SerializeField] RectTransform rect;
    [SerializeField] RectTransform sprite;
    [SerializeField] TMP_Text speakT;
    string speak;

    [SerializeField] float typingSpeed;

    public void Initialize(string speak)
    {
        this.speak = speak;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            StopAllCoroutines();
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Speak();   
    }
    
    void Speak()
    {
        IEnumerator Cor()
        {
            yield return StartCoroutine(UIMovement.Typing(speakT, speak, typingSpeed));

            yield return new WaitForSeconds(4f);

            Destroy(gameObject);
        }

        StartCoroutine(Cor());
    }
}
