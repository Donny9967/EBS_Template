using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMode : Popup
{
    [SerializeField]
    bool OnPractice;

    [SerializeField]
    GameObject GoToMain;
    [SerializeField]
    GameObject Dim;

    public override void OnEnable()
    {
        base.OnEnable();
        GoToMain.SetActive(false);
        Dim.SetActive(false);
    }

    public void EnterGoToMain(GameObject Target)
    {
        Dim.SetActive(true);
        Target.transform.localScale = Vector3.zero;
        Target.SetActive(true);
        Target.transform.DOMove(Vector3.zero, 0.2f);
        Target.transform.DOScale(1, 0.2f);
    }

    public void OnClickPractice()
    {
        OnPractice = true;
        popupManager.ModeConfirm(OnPractice, Number);
    }

    public void OnClickGame()
    {
        OnPractice = false;
        popupManager.ModeConfirm(OnPractice, Number);
    }
}
