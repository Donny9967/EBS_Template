using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : Popup
{

    [SerializeField]
    private bool OnPause;


    public void OnClick()
    {
        if (!OnPause) return;
        OnPause = false;
        GetComponentInParent<PopupManager>().ExitPopup();
    }

    public override void OnPopupActive()
    {
        OnPause = true;
    }
}
