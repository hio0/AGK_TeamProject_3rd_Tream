using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicIconData
{
    public Image strokeImage;
    public Image bgImage;
    public Image spriteImage;

    public CanvasGroup canvasGroup;
}

public class BasicIcon : MonoBehaviour
{
    public Image strokeImage;
    public Image bgImage;
    public Image spriteImage;

    public CanvasGroup can;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public BasicIconData ReturnImage() // 테토 접근법!!
    {
        BasicIconData data = new BasicIconData
        {
            strokeImage = strokeImage,
            bgImage = bgImage,
            spriteImage = spriteImage,
            canvasGroup = can
        };

        return data;
    }
}
