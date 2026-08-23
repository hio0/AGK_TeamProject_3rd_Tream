using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] RoomObject obj_enemyWave;

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

        FightManager.Instance.OnFighting += EventDiSet;
        FightManager.Instance.OnFightFinish += EventSet;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnNextClass -= EventSet;

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

            if (footstep >= 1f)
            {
                footstep -= 1f;
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
        RoomObject obj = null;
        int r = UnityEngine.Random.Range(1, 101);

        if(r <= 40)
        {
            obj = obj_itemBox;
        }
        else if(r >= 41 && r <= 90)
        {
            //obj = obj_enemyWave;
        }

        NodeData data = RoomManager.Instance.GetNodeData?.Invoke();
        if (data.nodeList.Count - 1 <= data.nowNodeNum) 
        {
            obj = obj_elevator;
        }

        parent_obj.anchoredPosition = obj_openPos;
        if (obj != null)
        {
           Instantiate(obj, parent_obj);
        }
    }
}
