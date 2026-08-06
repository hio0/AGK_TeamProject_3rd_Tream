using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public event Action OnPressingA;
    public event Action OnPressingD;
    public event Action OnPressTab;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            OnPressingA?.Invoke();
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnPressingD?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnPressTab?.Invoke();
        }
    }
}
