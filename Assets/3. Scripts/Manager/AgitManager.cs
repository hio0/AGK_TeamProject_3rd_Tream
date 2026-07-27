using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgitManager : MonoBehaviour
{
    [SerializeField] float camera_moveSpeed;
    [SerializeField] float loadSceneTime; 

    // Start is called before the first frame update
    void Start()
    {
        MainCamera.Instance.UpdateMove(MainSceneCameraMove);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MainSceneCameraMove()
    {
        float bojung = camera_moveSpeed;

        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 delta = (Vector2)Input.mousePosition - center;

        float x = Mathf.Clamp(delta.y / bojung, -4f, 4f);
        float y = Mathf.Clamp(delta.x / bojung, -3f, 3f);

        MainCamera.Instance.cameraTransform.rotation = Quaternion.Euler(-x, y, 0);
    }

    public void Attendance()
    {
        SceneMoveManager.Instance.FadeSceneLoad("School", loadSceneTime);
    }
}
