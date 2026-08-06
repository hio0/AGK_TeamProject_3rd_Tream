using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public string roomName;

    public Sprite wallSprite;
    public Sprite groundSprite;

    public RectTransform bg;
    public Image wall;
    public Image ground;

    public List<Transform> objectTransform;
    public List<RoomObject> objects;
    public List<Character> enemyWaves;

    Rigidbody2D rb;

    public int multiLine;

    public void Initialize(int  muliLine)
    {
        multiLine = muliLine;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnPressingA += StageBackMove;
        InputManager.Instance.OnPressingD += StageFowardMove;
    }
    private void OnDisable()
    {
        InputManager.Instance.OnPressingA -= StageBackMove;
        InputManager.Instance.OnPressingD -= StageFowardMove;
    }

    private void Start()
    {
        bg.GetComponent<Rigidbody2D>();
    }

    void StageFowardMove()
    {
        StageMove(true);
    }

    void StageBackMove()
    {
        StageMove(false);
    }

    void StageMove(bool isfoward)
    {
        Vector2 targetPos = Vector2.right;
        if(isfoward)
        {
            targetPos = Vector2.left;
        }

        rb.velocity += targetPos * 8f * Time.deltaTime;
    }
}
