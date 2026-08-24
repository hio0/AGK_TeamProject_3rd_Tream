using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;

    [SerializeField] float movespeed;
    [SerializeField] Renderer map;
    [SerializeField] RectTransform parent_obj;
    [SerializeField] Vector2 obj_openPos;
    [SerializeField] Vector2 obj_closePos;

    float footstep;
    float mapoffset;
    bool isStop;

    [SerializeField] RoomObject obj_elevator;
    [SerializeField] RoomObject obj_itemBox;
    [SerializeField] List<EnemyWaves> obj_enemyWave = new();
    [SerializeField] RoomObject obj_agitTrigger;

    public event Action OnMove;
    public event Action OnStop;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        parent_obj.anchoredPosition = obj_openPos;
        SchoolManager.instance.OnNextClass += EventSet;
        SchoolManager.instance.OnNextClass += ResetDate;

        FightManager.Instance.OnFighting += EventDiSet;
        FightManager.Instance.OnFightFinish += EventSet;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnNextClass -= EventSet;
        SchoolManager.instance.OnNextClass -= ResetDate;

        FightManager.Instance.OnFighting -= EventDiSet;
        FightManager.Instance.OnFightFinish -= EventSet;
        EventDiSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EventSet()
    {
        InputManager.Instance.OnPressD += Move;
        InputManager.Instance.OnPressA += Stop;
    }

    public void EventDiSet()
    {
        InputManager.Instance.OnPressD -= Move;
        InputManager.Instance.OnPressA -= Stop;
    }

    void ResetDate()
    {
        map.material.mainTextureOffset = Vector2.zero;
        for (int i = 0; i < parent_obj.childCount; i++)
        {
            Destroy(parent_obj.GetChild(i).gameObject);
        }
    }

    void Move()
    {
        isStop = true;
        isStop = false;
        StopAllCoroutines();
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while (!isStop)
        {
            float move = movespeed * Time.deltaTime;

            mapoffset += move;
            footstep += move;

            map.material.mainTextureOffset = new Vector2(mapoffset, map.material.mainTextureOffset.y);
            OnMove?.Invoke();

            if (footstep >= 1.5f)
            {
                footstep -= 1.5f;
                SetObject();

                NodeData data = RoomManager.Instance.GetNodeData?.Invoke();
                if (data.nodeList.Count > data.nowNodeNum)
                {
                    RoomManager.Instance.OnNodePass?.Invoke();
                }
            }

            yield return null;
        }
    }

    public void Stop()
    {
        isStop = true;
        OnStop?.Invoke();
    }

    void SetObject()
    {
        GameObject obj = null;
        int r = UnityEngine.Random.Range(1, 101);

        if(r <= 40)
        {
            obj = obj_itemBox.gameObject;
        }
        else if(r >= 41 && r <= 90)
        {
            obj = obj_enemyWave[UnityEngine.Random.Range(0, obj_enemyWave.Count)].gameObject;
        }

        NodeData data = RoomManager.Instance.GetNodeData?.Invoke();
        if (data.nodeList.Count - 1 <= data.nowNodeNum) 
        {
            if(ImportantData.nowFloorCount == ImportantData.maxFloorCount)
            {
                obj = obj_agitTrigger.gameObject;
            }
            else
            {
                obj = obj_elevator.gameObject;
            }
        }

        parent_obj.anchoredPosition = obj_openPos;
        if (obj != null)
        {
           Instantiate(obj, parent_obj);
        }
    }
}
