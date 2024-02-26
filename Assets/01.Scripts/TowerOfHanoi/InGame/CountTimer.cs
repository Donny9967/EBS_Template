using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountTimer : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI Text ������Ʈ�� �Ҵ��ϱ� ���� ����
    public TextMeshProUGUI countText; // UI Text ������Ʈ�� �Ҵ��ϱ� ���� ����
    private bool isTiming = false; // �ð� ���� ���θ� �Ǵ��ϴ� ����
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

    // ī��Ʈ ���� ��Ű�� �Լ�
    public void CountUp()
    {
        moveCount++;
        countText.text = moveCount.ToString();
    }

    // Ÿ�̸� �����ϴ� �Լ�
    public void StartTime()
    {
        if (GameManager.Instance.OnTutorial) return;

        isTiming = true;
    }

    // �ð� ������ ������ �� ȣ��
    public void StopTime()
    {
        isTiming = false;
    }

    // �ð��� 0���� ����� �Լ�
    public void ResetTime()
    {
        t = 0;
    }

    // 0���� ������ �ٷ� ���� �ϴ� �Լ�
    public void ResetStartTime()
    {
        t = 0;
        isTiming = true;
    }


    void Update()
    {
        if (isTiming)
        {
            t = t + Time.deltaTime; // ��� �ð��� ���

            TimeSpan timeSpan = TimeSpan.FromSeconds(t);

            // �и��ʸ� �� �ڸ��� �ݿø�
            int roundedMilliseconds = (int)Math.Round(timeSpan.TotalMilliseconds % 1000 / 10);

            // �ð�, ��, ��, �и���(�� �ڸ�) �������� ��ȯ
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                roundedMilliseconds);

            scoreText.text = formattedTime;
        }
    }

    // ���� Ŭ���� ���� ���� �ð�
    public void OnGameClearTime()
    {
        StopTime();
        
        GameManager.Instance.Score = scoreText.text;

    }

    // ���� Ŭ���� ���� �� ������ Ƚ��
    public void OnGameClearCount()
    {
        GameManager.Instance.Count = moveCount;
    }

}
