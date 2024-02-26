using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : Popup
{
    public int stageNumber = 1;
    [SerializeField]
    public List<Toggle> toggles = new List<Toggle>();

    public void OnClickOK()
    {
        popupManager.StageConfirm(stageNumber, Number);

    }

    public override void Init()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].isOn = false;
        }
        toggles[0].isOn = true;
    }

    public override void OnClickExit()
    {
        if (OnPopup)
        {
            popupManager.ExitPopup();
        }

        transform.DOLocalMove(Vector3.zero, 0.2f);
        transform.DOScale(0, 0.2f).OnComplete(CompleteFunction);
    }

    public void CompleteFunction()
    {
        gameObject.SetActive(false);
    }

    public void OnClickStage(int StageNumber)
    {
        stageNumber = StageNumber;
    }

    public void OnClickAgain()
    {
        if (!GameManager.Instance.OnPractice)
        {
            GameManager.Instance.API4();

            popupManager.StageConfirm(3, Number);
            GameManager.Instance.API1();
        }
        else
        {
            popupManager.StageConfirm(GameManager.Instance.Stage, Number);
        }

    }
}
