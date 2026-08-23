using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class StoryCutScene : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Vector2 openPos;
    [SerializeField] Vector2 closePos;
    [SerializeField] float animationSpeed;

    [SerializeField][TextArea] List<string> storys = new();
    [SerializeField] TMP_Text stroyT;
    [SerializeField] float typingSpeed;
    [SerializeField] GameObject next;

    [SerializeField] TMP_Text stuckInSchoolT;
    [SerializeField] TMP_Text dayT;

    [SerializeField] List<Character> defultSetChar;
    [SerializeField] float loadSceneTime;

    int storyNum;
    Coroutine cor_typing;

    // Start is called before the first frame update
    void Start()
    {
        storyNum = 0;
        rect.anchoredPosition = closePos;
        stroyT.gameObject.SetActive(false);
        next.SetActive(false);
        stuckInSchoolT.gameObject.SetActive(false);
        dayT.gameObject.SetActive(false);

        MoveIn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MoveIn()
    {
        IEnumerator Cor()
        {
            UIMovement.DoAnchorMove(rect, openPos, animationSpeed);
            next.SetActive(true);

            yield return new WaitForSeconds(animationSpeed + 1f);

            Storys();
        }

        StartCoroutine(Cor());
    }

    public void Storys()
    {
        if (cor_typing != null)
        {
            StopCoroutine(cor_typing);
            cor_typing = null;
        }

        if (storyNum >= storys.Count)
        {
            next.SetActive(false);

            IEnumerator Cor()
            {
                yield return new WaitForSeconds(1f);

                stuckInSchoolT.gameObject.SetActive(true);
                stuckInSchoolT.maxVisibleCharacters = 3;

                yield return new WaitForSeconds(1f);

                stuckInSchoolT.maxVisibleCharacters = stuckInSchoolT.textInfo.characterCount;

                yield return new WaitForSeconds(1f);

                dayT.gameObject.SetActive(true);

                yield return new WaitForSeconds(1f);

                DefultSet();
                SceneMovement.LoadLoadingScene("School");
            }

            StartCoroutine(Cor());
        }
        else
        {
            stroyT.gameObject.SetActive(true);

            cor_typing = StartCoroutine(UIMovement.Typing(stroyT, storys[storyNum], typingSpeed));
            storyNum++;
        }
    }

    void DefultSet()
    {
        ImportantData.SetDefultValue();
        ImportantData.canUsedStudents = defultSetChar;
    }
}
