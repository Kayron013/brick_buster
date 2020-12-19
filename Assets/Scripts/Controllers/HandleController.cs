using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour
{
    private GameController controller;
    private Rigidbody2D rb;

    private Vector2 startingPos = new Vector2(4.4244f, -3.9708f);

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.WaveIsActive)
        {
            var leftBound = 2.0868f;
            var rightBound = 6.5651f;

            var minX = leftBound;
            var maxX = rightBound;

            var mouseX = Utils.GetMousePosition().x;
            var pos = rb.transform.position;
            var newX = Mathf.Clamp(mouseX, minX, maxX);
            rb.transform.position = new Vector2(newX, pos.y);
        }
    }

    public void Reset()
    {
        gameObject.transform.position = startingPos;
    }
}
