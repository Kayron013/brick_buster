using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickController : MonoBehaviour
{
    public const float Size = 0.5685724f;

    public int _value;
    private new Renderer renderer;
    private GameController controller;

    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
        renderer = GetComponent<Renderer>();
        //Debug.Log($"Width: {renderer.bounds.size.x}");
        //Debug.Log($"Height: {renderer.bounds.size.y}");

    }

    // Start is called before the first frame update
    void Start()
    {
        Value = _value;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Value--;
        }
    }

    public int Value { 
        get => _value; 
        set
        {
            _value = value;
            OnValueChange();
        }
    }

    private void OnValueChange()
    {
        if (Value < 1)
        {
            Destroy(gameObject);
        }
        else
        {
            var text = gameObject.GetComponentInChildren<Text>();
            text.text = Value.ToString();

            renderer.material.color = GetColorsFromValue(Value);
        }
    }

    private Color GetColorsFromValue(int val)
    {
        float maxH = .9f; // 100% in custom color range is 90% (pink) in full color range

        float perc = val / 10f;
        float valInCustomRange = Mathf.Clamp(perc * maxH, 0, maxH);

        float h = valInCustomRange;
        return Color.HSVToRGB(h, 1, 1);
    }

    private void OnDestroy()
    {
        controller.BrickDestroyed();
    }
}
