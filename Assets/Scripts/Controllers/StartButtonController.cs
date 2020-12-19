using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }
    public void Show()
    {
        button.gameObject.SetActive(true);
    }

    public void Hide()
    {
        button.gameObject.SetActive(false);
    }
}
