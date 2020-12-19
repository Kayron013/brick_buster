using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : MonoBehaviour
{
    public static BrickController brick { private get; set; }

    // Start is called before the first frame update
    public static int Generate(int rows, int cols)
    {
        var numBricks = 0;
        var startPosition = new Vector2(1.76379f, 0.4862828f);
        var brickSize = BrickController.Size;

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                var value = Random.Range(1, 10);
                if (value == 0) continue;

                var _brick = Instantiate(brick);
                numBricks++;

                var xPos = startPosition.x + col * brickSize;
                var yPos = startPosition.y + row * brickSize;
                var position = new Vector2(xPos, yPos);

                _brick.GetComponent<Rigidbody2D>().transform.position = position;
                _brick.Value = value;
            }

        }
        return numBricks;
    }
}
