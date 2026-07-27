using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public event Action OnPressA;
    public event Action OnPressD;
    public event Action OnPressSemicolon;

    private void Awake()
    {
        Instance = this;
        ClearEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            OnPressA?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnPressD?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Semicolon))
        {
            OnPressSemicolon?.Invoke();
            GameEvent.OnNoticedSomething?.Invoke("이재용은(는) 유니티의 공격에 몹시 화난 듯 하다.");
        }
    }

    public void ClearEvent()
    {
        OnPressA = null;
        OnPressD = null;
    }
}
