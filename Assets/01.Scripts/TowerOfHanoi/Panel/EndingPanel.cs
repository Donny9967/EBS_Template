using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingPanel : Panel
{
    GameClear gameClear;
    RankingPanel rankingPanel;

    [SerializeField]
    Image Dim;
    [SerializeField]
    Image TestDim;

    float dimAlpha;

    public void Awake()
    {
        transform.localPosition = Vector3.zero;

        gameClear = GetComponentInChildren<GameClear>();
        gameClear.transform.DOScale(0, 0);

        rankingPanel = GetComponentInChildren<RankingPanel>();
        rankingPanel.transform.DOScale(0, 0);

        dimAlpha = Dim.color.a;
        TestDim.gameObject.SetActive(false);
    }

    public void Init()
    {
        
        transform.DOScale(0,0);
        Dim.DOFade(0, 0);

        gameClear.Init();
    }

    public void ClearGame()
    {
        gameClear.transform.DOScale(1, 0);
        gameClear.ClearGame();

        transform.localScale = Vector3.zero;
        transform.DOLocalMove(Vector3.zero, 0.2f);
        transform.DOScale(1, 0.2f);

    }

    public void OnClickAgain()
    {
        transform.DOLocalMove(Vector3.zero, 0.2f);
        transform.DOScale(0, 0.2f);
    }

    public void OnClickMain()
    {
        if (!GameManager.Instance.OnPractice)
            GameManager.Instance.API4();

        transform.DOLocalMove(Vector3.zero, 0.2f);
        transform.DOScale(0, 0.2f);
        GameManager.Instance.Init();
    }

    public void OnClickRanking()
    {
        GameManager.Instance.API5(RankingAppear);
    }

    public void RankingAppear()
    {
        rankingPanel.SetStageText();
        Dim.DOFade(dimAlpha, 0);
        rankingPanel.transform.DOLocalMove(Vector3.zero, 0.2f);
        rankingPanel.transform.DOScale(1, 0.2f);
    }

    public void OnClickExitRanking()
    {
        Dim.DOFade(0, 0);
        rankingPanel.transform.DOLocalMove(Vector3.zero, 0.2f);
        rankingPanel.transform.DOScale(0, 0.2f).OnComplete(AfterExitRanking);
    }

    public void AfterExitRanking()
    {
        for (int i = 0; i < rankingPanel.rankings.Count; i++)
        {
            rankingPanel.rankings[i].name.text = null;
            rankingPanel.rankings[i].score.text = null;
        }
    }

}
