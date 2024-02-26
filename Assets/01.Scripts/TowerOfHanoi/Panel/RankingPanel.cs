using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingPanel : Panel
{
    public List<Ranking> rankings = new List<Ranking>();

    [SerializeField]
    TextMeshProUGUI titleStage;

    public virtual void Awake()
    {
        transform.localPosition = Vector3.zero;
        GetComponentsInChildren(rankings);
        SetNumber();
    }

    void SetNumber()
    {
        for (int i = 0; i < rankings.Count; i++)
        {
            rankings[i].number.text = (i + 1).ToString();
        }
    }

    public void SetStageText()
    {
        if (titleStage != null)
            titleStage.text = "¿øÆÇ " + GameManager.Instance.Stage.ToString() + "°³";
    }
}
