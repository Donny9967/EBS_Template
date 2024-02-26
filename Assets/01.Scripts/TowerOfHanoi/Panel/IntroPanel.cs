using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPanel : Panel
{
    [SerializeField]
    Transform Title;

    bool oneTime;

    private void Awake()
    {
        transform.localPosition = Vector3.zero;
    }

    public void Init()
    {
        transform.DOScale(1, 0);
        oneTime = false;
    }

    public void OnClickStart()
    {
        if (oneTime) return;
        oneTime = true;
        Sequence mySequence = DOTween.Sequence()
            // 움직임 설정

            .AppendCallback(APICallbak);
    }


    public void APICallbak()
    {
        GameManager.Instance.API1(() =>
        {
            GameManager.Instance.API6();
        });

        transform.DOScale(0, 0);
    }



}
