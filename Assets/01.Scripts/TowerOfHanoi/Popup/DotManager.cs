using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotManager : MonoBehaviour
{
    [SerializeField]
    Popup_Contents Content;

    [SerializeField]
    List<Image> dotImg = new List<Image>();

    [SerializeField]
    Sprite InActiveDot;
    [SerializeField]
    Sprite ActiveDot;

    public void Init()
    {
        GameObject Dot = Resources.Load<GameObject>("Dot");

        for (int i = 0; i < Content.contents.Count; i++)
        {
            dotImg.Add(Instantiate(Dot, transform).GetComponent<Image>());
        }
    }

    public void CheckDot(int ContentNumber)
    {
        for (int i = 0; i < dotImg.Count; i++)
        {
            dotImg[i].sprite = InActiveDot;
        }
        dotImg[ContentNumber - 1].sprite = ActiveDot;
    }

}
