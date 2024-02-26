using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMain : Popup
{
    public void ModeExit()
    {
        transform.DOLocalMove(Vector3.zero, 0.2f);
        transform.DOScale(0, 0.2f);
    }
}
