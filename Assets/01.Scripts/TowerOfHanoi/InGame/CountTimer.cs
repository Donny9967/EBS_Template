using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountTimer : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI Text 컴포넌트를 할당하기 위한 변수
    public TextMeshProUGUI countText; // UI Text 컴포넌트를 할당하기 위한 변수
    private bool isTiming = false; // 시간 측정 여부를 판단하는 변수
    [SerializeField]
    private float t;
    [SerializeField]
    private int moveCount;
    void Start()
    {
        t = 0;
        moveCount = 0;
    }

    public void Init()
    {
        isTiming = false;
        t = 0;
        moveCount = 0;
        countText.text = moveCount.ToString();
        scoreText.text = "00:00:00.00";

        OnGameClearTime();
        OnGameClearCount();
    }

    // 카운트 증가 시키는 함수
    public void CountUp()
    {
        moveCount++;
        countText.text = moveCount.ToString();
    }

    // 타이머 시작하는 함수
    public void StartTime()
    {
        if (GameManager.Instance.OnTutorial) return;

        isTiming = true;
    }

    // 시간 측정을 중지할 때 호출
    public void StopTime()
    {
        isTiming = false;
    }

    // 시간을 0으로 만드는 함수
    public void ResetTime()
    {
        t = 0;
    }

    // 0으로 돌리고 바로 시작 하는 함수
    public void ResetStartTime()
    {
        t = 0;
        isTiming = true;
    }


    void Update()
    {
        if (isTiming)
        {
            t = t + Time.deltaTime; // 경과 시간을 계산

            TimeSpan timeSpan = TimeSpan.FromSeconds(t);

            // 밀리초를 두 자리로 반올림
            int roundedMilliseconds = (int)Math.Round(timeSpan.TotalMilliseconds % 1000 / 10);

            // 시간, 분, 초, 밀리초(두 자리) 포맷으로 변환
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                roundedMilliseconds);

            scoreText.text = formattedTime;
        }
    }

    // 게임 클리어 했을 때의 시간
    public void OnGameClearTime()
    {
        StopTime();
        
        GameManager.Instance.Score = scoreText.text;

    }

    // 게임 클리어 했을 때 움직인 횟수
    public void OnGameClearCount()
    {
        GameManager.Instance.Count = moveCount;
    }

}
