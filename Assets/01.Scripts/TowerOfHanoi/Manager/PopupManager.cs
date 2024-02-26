using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    InGamePanel ingamePanel;

    [SerializeField]
    public int PopupNumber;

    [SerializeField]
    Transform Pivot_Home;
    private Transform Prev_Pos;

    Popup[] inPopupChildren;
    [SerializeField]
    List<Popup> inPopups = new List<Popup>();

    [SerializeField]
    public Image Dim_Dark;

    public MonoBehaviour targetScript;


    private void Awake()
    {
        ingamePanel = FindObjectOfType<InGamePanel>();
        transform.localPosition = Vector3.zero;

        inPopupChildren = GetComponentsInChildren<Popup>();

        // 현재 오브젝트의 모든 자식 오브젝트를 순회
        foreach (Popup child in inPopupChildren)
        {
            // 자식 오브젝트에 대한 작업 수행
            if (child.GetComponent<Popup>().onTutorial)
            {
                inPopups.Add(child.GetComponent<Popup>());
            }
            // 여기에 추가적인 코드를 작성할 수 있습니다.
        }

        //Init();
    }

    // 초기화
    public void Init()
    {
        transform.DOScale(1, 0);

        PopupNumber = 0;

        for (int i = 0; i < inPopupChildren.Length; i++)
        {
            inPopupChildren[i].Number = i;
            inPopupChildren[i].Init();
            inPopupChildren[i].gameObject.SetActive(false);
        }

        inPopupChildren[0].gameObject.SetActive(true);
        inPopupChildren[0].transform.DOLocalMove(Vector3.zero, 0.2f);
        inPopupChildren[0].transform.DOScale(1, 0.2f);



    }

    // 메뉴눌러서 팝업 활성화 할 때 켜지는 효과
    public void EnterPopup(Transform Pivot)
    {
        Prev_Pos = Pivot;
        transform.position = Pivot.transform.position;
        transform.DOLocalMove(Vector3.zero, 0.2f);
        transform.DOScale(1, 0.2f);
    }

    // 메뉴 눌러서 팝업 활성화
    public void EnterPopupActive(GameObject Target)
    {
        DimFadeIn();

        for (int i = 0; i < inPopups.Count; i++)
        {
            inPopups[i].gameObject.SetActive(false);
        }

        if (Target.GetComponent<Popup>())
        {
            Target.transform.localScale = Vector3.zero;
            Target.SetActive(true);
            Popup pop = Target.GetComponent<Popup>();
            pop.OnPopup = true;
            pop.OnPopupActive();
            Target.transform.DOScale(1, 0.2f).OnComplete(() => PopupNumber = pop.Number);
        }

    }

    // 팝업 닫기 버튼
    public void ExitPopup()
    {
        if (GameManager.Instance.OnTutorial)
        {
            // 튜토리얼의 마지막 팝업이라면
            if (PopupNumber >= inPopups.Count - 1)
            {
                //inPopups[PopupNumber].gameObject.SetActive(false);
                //Dim.SetActive(false);

                DimFadeOut();
                // 작아지기
                transform.DOMove(Pivot_Home.transform.position, 0.2f);
                transform.DOScale(0, 0.2f);
            }
            else
            {
                inPopups[PopupNumber].transform.DOScale(0, 0.2f).OnComplete(TutoCompleteFunction);
                inPopups[PopupNumber + 1].gameObject.SetActive(true);
                inPopups[PopupNumber + 1].transform.localScale = Vector3.zero;
                inPopups[PopupNumber + 1].transform.DOScale(1, 0.2f);
                inPopups[PopupNumber + 1].OnPopupActive();
                PopupNumber++;
            }
        }
        // 튜토리얼이 아니라 인게임 상황이라면
        else
        {
            DimFadeOut();
            if (Prev_Pos != null)
            {
                transform.DOMove(Prev_Pos.transform.position, 0.2f);
                transform.DOScale(0, 0.2f).OnComplete(CompleteFunction);
            }
            else
            {
                transform.DOMove(Pivot_Home.transform.position, 0.2f);
                transform.DOScale(0, 0.2f).OnComplete(CompleteFunction);
            }

        ;
        }

    }

    public void TutoCompleteFunction()
    {
        inPopups[PopupNumber - 1].gameObject.SetActive(false);
    }
    public void CompleteFunction()
    {
        inPopupChildren[PopupNumber].gameObject.SetActive(false);
    }

    // 이름 입력
    public void NameConfirm(string PlayerName, int popNumber)
    {
        GameManager.Instance.Username = PlayerName;
        NextPopup(popNumber);
    }

    // 모드 선택
    public void ModeConfirm(bool PracticeMode, int popNumber)
    {

        GameManager.Instance.OnPractice = PracticeMode;

        if (PracticeMode)
        {
            NextPopup(popNumber);
        }
        else
        {
            NextPopup(popNumber + 1);
        }
    }

    // 스테이지 선택
    public void StageConfirm(int StageNumber, int popNumber)
    {
        GameManager.Instance.Stage = StageNumber;
        GameManager.Instance.API4();

        //ingamePanel.gameObject.SetActive(true);

        NextPopup(popNumber + 1);
    }

    public void NextPopup(int popNumber)
    {
        // 마지막 팝업이라면
        if (popNumber >= inPopups.Count - 1)
        {
            for (int i = 0; i < popNumber; i++)
            {
                inPopups[i].gameObject.SetActive(false);
            }
            // 방금 팝업 끄고
            transform.DOLocalMove(Vector2.zero, 0.2f);
            transform.DOScale(0, 0.2f);

            // 딤처리 끄고
            DimFadeOut();

            // 튜토리얼을 끝낸다
            GameManager.Instance.OnTutorial = false;

            ingamePanel.gameObject.SetActive(false);
            // 마지막 팝업이 끝나고 게임을 시작한다
            ingamePanel.gameObject.SetActive(true);
        }
        else
        {
            // 마지막 팝업이 아니라 뒤에 더 있다면
            // 현재 팝업을 끄고
            inPopups[popNumber].transform.DOScale(0, 0.2f).OnComplete(() => NextPopupCompleteFunction(popNumber));

            // 다음 팝업을 키고
            inPopups[PopupNumber + 1].transform.localScale = Vector2.zero;
            inPopups[popNumber + 1].gameObject.SetActive(true);
            inPopups[PopupNumber + 1].transform.DOScale(1, 0.2f);
            // 현재의 팝업 번호를 1 증가한다
            PopupNumber++;

        }
    }

    public void NextPopupCompleteFunction(int popNumber)
    {
        inPopups[popNumber].gameObject.SetActive(false);
    }


    public void DimFadeIn()
    {
        targetScript.enabled = false;
        Dim_Dark.raycastTarget = true;
        Dim_Dark.DOFade(1, 0.2f);
    }

    public void DimFadeOut()
    {
        Dim_Dark.DOFade(0, 0.2f).OnComplete(() =>
        {
            targetScript.enabled = true;
            Dim_Dark.raycastTarget = false;
        });
    }
}
