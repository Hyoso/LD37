using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorControl : MonoBehaviour
{
    Vector2 prevScreenSize;

    void Start()
    {
        prevScreenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update ()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (prevScreenSize.x != Screen.width || prevScreenSize.y != Screen.height)
        {
            ResetPosition();
            prevScreenSize = new Vector2(Screen.width, Screen.height);
        }
    }

    void ResetPosition()
    {
        transform.position = Vector3.zero;
    }
}
