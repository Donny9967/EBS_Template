using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    private Image img;

    [SerializeField]
    private bool isOn = true;

    [SerializeField]
    Sprite Active;
    [SerializeField]
    Sprite InActive;

    void Start()
    {
        img = GetComponent<Image>();
    }

    public void OnClick()
    {
        if (isOn)
        {
            isOn = false;
            img.sprite = InActive;
            AudioListener.volume = 0;
        }
        else
        {
            isOn = true;
            img.sprite = Active;
            AudioListener.volume = 1;
        }
    }
}
