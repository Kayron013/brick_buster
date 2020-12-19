using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private new Collider2D collider;
    private GameController controller;

    public static Vector2 startingBallPos = new Vector2(4.42f, -3.972477f);
    public static float InitialSpeed { get; } = 7;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        Reset();
    }

    public void Reset()
    {
        rb.velocity = new Vector2();
        gameObject.transform.position = startingBallPos;
    }
    public void Launch()
    {
        var mousePos = Utils.GetMousePosition();
        var minY = startingBallPos.y + .35f;
        var targetPos = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y,minY, 1000f));
        var ballPos = rb.position;
        var vector = (targetPos - ballPos).normalized;
        rb.velocity = vector * InitialSpeed;
    }

    public void IncreaseSpeed()
    {
        var initialV = rb.velocity;
        var currentSpeed = initialV.magnitude;
        var velocity = initialV.normalized * (currentSpeed + .15f);
        rb.velocity = velocity;
    }

    float lastY;
    int stuckCounter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Physics2D.IgnoreCollision(collision.collider, collider);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            controller.RestartRound();
        }
        // Check if ball is stuck on a y value, add force if necessary.
        else if (collision.gameObject.CompareTag("Wall"))
        {
            var y = rb.transform.position.y;
            if (y == lastY)
            {
                stuckCounter++;
                if(stuckCounter > 3)
                {
                    var x = rb.velocity.x > 0 ? 1 : -1; // keep x direction
                    rb.AddForce(new Vector2(x, -1));
                    stuckCounter = 1;
                }
            }
            else
            {
                stuckCounter = 1;
            }
            lastY = y;
        }
        // Make the ball bounce further to the side the futher away from the center it hits the handle.
        else if (collision.gameObject.CompareTag("Handle"))
        {
            var handle = collision.gameObject;
            var handleBounds = handle.GetComponent<Renderer>().bounds;
            var handleCenter = handleBounds.center.x;
            var handleRadius = handleBounds.extents.x;

            var distFromCenter = Mathf.Abs(gameObject.transform.position.x - handleCenter);
            var percFromCenter = distFromCenter / handleRadius;

            var x = (handleCenter > gameObject.transform.position.x ? -1 : 1) * percFromCenter;
            var y = 1f;
            var currentSpeed = rb.velocity.magnitude;
            rb.velocity = new Vector2(x, y).normalized * currentSpeed;
        }
    }
}
