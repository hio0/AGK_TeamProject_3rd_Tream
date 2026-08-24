using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiddleManager : MonoBehaviour
{
    [SerializeField] GameObject elevatorBg;
    [SerializeField] GameObject agitBg;
    [SerializeField] GameObject followPartners;
    [SerializeField] TMP_Text floorT;
    [SerializeField] TMP_Text nextFloorT;

    [SerializeField] GameObject backToAgit;
    [SerializeField] GameObject nextFloor;
    [SerializeField] GameObject startSchool;

    public void Initialize(bool isEle)
    {
        if(isEle)
        {
            followPartners.SetActive(false);
            elevatorBg.SetActive(true);
            agitBg.SetActive(false);

            backToAgit.SetActive(true);
            nextFloor.SetActive(true);
            startSchool.SetActive(false);

            nextFloorT.text = $"앞으로 {ImportantData.maxFloorCount - ImportantData.nowFloorCount}F";
            floorT.text = $"{ImportantData.nowFloorCount} > {ImportantData.nowFloorCount + 1}";
        }
        else
        {
            followPartners.SetActive(true);
            elevatorBg.SetActive(false);
            agitBg.SetActive(true);

            backToAgit.SetActive(false);
            nextFloor.SetActive(false);
            startSchool.SetActive(true);

            RangeManager.instance.RangeClear();
        }
    }

    private void OnDisable()
    {
        elevatorBg.SetActive(false);
        agitBg.SetActive(false);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        FadePanel.instance.Canvas(0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextFloor()
    {
        ImportantData.nowFloorCount++;
        SceneChange.instance.Move(false);

        IEnumerator Cor()
        {
            yield return new WaitForSeconds(1f);

            elevatorBg.SetActive(false);
            gameObject.SetActive(false);
            SchoolManager.instance.OnNextFloor.Invoke();
        }
        StartCoroutine(Cor());
    }

    public void Started()
    {
        SchoolManager.instance.SetStart();
    }

    public void ReturnToAgit()
    {
        IEnumerator Cor()
        {
            Map.Instance.Stop();
            Map.Instance.EventDiSet();

            SceneChange.instance.Move(false);

            yield return new WaitForSeconds(1.5f);

            SchoolManager.instance.NextDay();
            SchoolManager.instance.OnAgitScene?.Invoke();
        }

        StartCoroutine(Cor());
    }
}
