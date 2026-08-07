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

    public List<RectTransform> objectTransform;
    public List<RoomObject> objects;
    public List<EnemyWave> enemyWaves;

    Rigidbody2D rb;
    public int footstep;

    private void OnEnable()
    {
        PlusEvent();

        FightManager.Instance.OnFighting += RemoveEvent;
        FightManager.Instance.OnFightFinish += PlusEvent;
    }
    private void OnDisable()
    {
        RemoveEvent();

        FightManager.Instance.OnFighting -= RemoveEvent;
        FightManager.Instance.OnFightFinish -= PlusEvent;
    }

    private void Start()
    {
        rb = bg.GetComponent<Rigidbody2D>();
    }

    void PlusEvent()
    {
        InputManager.Instance.OnPressingA += StageBackMove;
        InputManager.Instance.OnPressingD += StageFowardMove;
    }

    void RemoveEvent()
    {
        InputManager.Instance.OnPressingA -= StageBackMove;
        InputManager.Instance.OnPressingD -= StageFowardMove;
    }

    void StageFowardMove()
    {
        StageMove(true);

        footstep++;
        if (footstep >= 200)
        {
            footstep = 0;
            int r = Random.Range(1, 101);

            if(r <= 20)
            {
                FightManager.Instance.OnFighting?.Invoke();
                footstep = 0;
            }
        }
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
