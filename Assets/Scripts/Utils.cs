using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector2 GetMousePosition()
    {
        var mousePos3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePos3.x, mousePos3.y);
    }
}
