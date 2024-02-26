using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tip : MonoBehaviour
{
    private TextMeshProUGUI txt;

    private void Start()
    {
        transform.localScale = Vector3.zero;   
        txt = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(RectTransform Pivot)
    {
        transform.position = Pivot.position;
        transform.DOMove(Pivot.position, 0.2f);
        transform.DOScale(1, 0.2f).Delay();
    }

    public void OnPointerText(string text)
    {
        txt.text = text;
    }

    public void OnPointerExit(RectTransform Pivot)
    {        
        transform.position = Pivot.position;
        transform.DOMove(Pivot.position, 0.2f);
        transform.DOScale(0, 0.2f).Delay();
    }

}
