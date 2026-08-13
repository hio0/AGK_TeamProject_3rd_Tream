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
    RectTransform rect;
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
        rect = bg.GetComponent<RectTransform>();
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
        if (footstep >= 200 && rect.anchoredPosition.x > -1330)
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

    void StageMove(bool isForward)
    {
        float direction = isForward ? -1f : 1f;

        Vector2 pos = bg.anchoredPosition;
        pos.x += direction * 300f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -1330f, 0f);

        bg.anchoredPosition = pos;
    }
}
