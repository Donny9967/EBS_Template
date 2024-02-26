using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    [Space(10)]
    [SerializeField]
    TextMeshProUGUI record_Title;
    [SerializeField]
    TextMeshProUGUI record_Name;
    [SerializeField]
    TextMeshProUGUI record_Count;
    [SerializeField]
    TextMeshProUGUI record_Score;

    public void Awake()
    {
        transform.localPosition = Vector3.zero;
        Init();
    }


    public void Init()
    {
        record_Title.text = null;
        record_Name.text = null;
        record_Count.text = null;
        record_Score.text = null;
    }

    public void ClearGame()
    {
        SetScore(GameManager.Instance.Score);
        SetCount(GameManager.Instance.Count);
        SetName(GameManager.Instance.Username);
        SetTitle(GameManager.Instance.Stage);
    }


    public void SetCount(int recordCount)
    {
        record_Count.text = recordCount.ToString();
    }

    public void SetScore(string totalScore)
    {
        record_Score.text = totalScore;
    }

    public void SetName(string recordName)
    {
        record_Name.text = recordName + " 님의 기록은";
    }

    public void SetTitle(int stageNumber)
    {
        record_Title.text = "원판 " + stageNumber.ToString() + "개를 옮기는데 성공했습니다.";
    }

}
