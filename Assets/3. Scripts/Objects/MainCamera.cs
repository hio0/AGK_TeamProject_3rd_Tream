using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance;
    public Camera cam;

    public GameObject cameraObj;
    public Transform cameraTransform;

    public bool canMove;
    public event Action OnUpdateMove;

    private void Awake()
    {
        Instance = this;

        cam = GetComponent<Camera>();
        cameraObj = gameObject;
        cameraTransform = gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMove(Action updateMove) // Update에서 계속 Invoke하면 메모리 누수나니 따로 Update 역할의 함수 만듬
    {
        canMove = true;
        OnUpdateMove -= updateMove;
        OnUpdateMove += updateMove;

        IEnumerator CorrectWhile()
        {
            while (canMove)
            {
                OnUpdateMove?.Invoke();
                yield return null;
            }
        }

        StartCoroutine(CorrectWhile());
    }

    public void StopMove()
    {
        canMove = false;

        OnUpdateMove = null;
    }
}
