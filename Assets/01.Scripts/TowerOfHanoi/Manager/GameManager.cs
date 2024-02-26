using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class Api1Response
{
    public string CODE;
    public ResultData1 RESULT;
}

[System.Serializable]
public class ResultData1
{
    public int pkgOrgStdyTracSno;
    public int attendDtlId;
    public int rscSno;
}

[System.Serializable]
public class Api2Response
{
    public string CODE;
    public string RESULT;
}


[System.Serializable]
public class Api5Response
{
    public string CODE;
    public List<RankingData> LIST;
}

[System.Serializable]
public class RankingData
{
    public string GAME_TYPE;
    public string BEST_RECORD;
    public string STAGE_STEP;
    public int USER_RANK;
    public int RSC_SNO;
    public string USER_NAME;
    public int TRAC_SNO;
    public string GAME_NAME;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;



    [SerializeField]
    public bool OnTutorial;
    [SerializeField]
    public bool onPractice;
    [SerializeField]
    private string userName;
    [SerializeField]
    private int stage;
    [SerializeField]
    private int level;
    [SerializeField]
    private string score;
    [SerializeField]
    private int count;



    public bool OnPractice
    {
        get { return onPractice; }
        set { onPractice = value; }
    }

    public string Username
    {
        get { return userName; }
        set { userName = value; }
    }

    public int Stage
    {
        get { return stage; }
        set
        {
            if (_gameType == "C")
            {
                stage = 1;
            }
            else if (_gameType == "B")
            {
                stage = value;
            }
        }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public string Score
    {
        get { return score; }
        set
        {
            score = value;
        }
    }

    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    [Space(20), Header("Panel")]
    [SerializeField] IntroPanel introPanel;
    [SerializeField] TopPanel topPanel;
    [SerializeField] InGamePanel inGamePanel;
    [SerializeField] EndingPanel endingPanel;
    [SerializeField] RankingPanel rankingPanel;
    [SerializeField] CountTimer countTimer;

    [Space(20), Header("Manager")]
    [SerializeField] WebManager webManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] PopupManager popupManager;

    string _baseUrl = "https://www.ebsmath.co.kr";

    [Space(10)]
    [Header("Input")]
    [SerializeField]
    bool isTest;
    [SerializeField]
    int _rscSno;
    [SerializeField]
    string _historyYn = "study";
    [SerializeField]
    string _evtSsnCd = "";
    [SerializeField]
    string _gameType = "B";
    [SerializeField]
    string _divType = "PC";

    [Space(10)]
    [SerializeField]
    long _startDate;
    [SerializeField]
    long _endDate;

    [Space(10)]
    [Header("Get")]
    [SerializeField]
    string _PTS_PRS_PRD = ""; // Base64.encode(게임시작시 pkgOrgStdyTracSno값)


    [Space(10)]
    [Header("GetResult")]
    [SerializeField]
    int _pkgOrgStdyTracSno;
    [SerializeField]
    int _attendDtlId;


    private void Awake()
    {
        Instance = this;

        if (isTest)
        {
            _rscSno = 29447;
            _gameType = "C";
        }

        introPanel = GetComponentInChildren<IntroPanel>();
        topPanel = GetComponentInChildren<TopPanel>();
        inGamePanel = GetComponentInChildren<InGamePanel>();
        endingPanel = GetComponentInChildren<EndingPanel>();
        rankingPanel = GetComponentInChildren<RankingPanel>();
        countTimer = GetComponentInChildren<CountTimer>();
    }

    void Start()
    {
        switch (_gameType)
        {
            case "A":
                break;
            case "B":
                Clear += countTimer.OnGameClearTime;
                Clear += countTimer.OnGameClearCount;
                break;
            case "C":
                break;
        }

        Clear += endingPanel.ClearGame;

        Init();
    }


    public void Init()
    {
        OnTutorial = true;
        userName = null;
        stage = 0;

        popupManager.Init();
        introPanel.Init();
        inGamePanel.Init();
        inGamePanel.gameObject.SetActive(false);
        endingPanel.Init();
    }

    public delegate void ClearDelegate();
    public event ClearDelegate Clear;

    // 게임을 클리어 하면 호출하는 함수
    public void GameClear(bool clear)
    {
        Clear?.Invoke();

        API2(() =>
        {
            API3(() =>
            {
                API4();
            });
        });
    }


    #region API

    public void API1(Action callback = null)
    {
        webManager.Get_1StudyHistory("GET", _baseUrl, _rscSno, _historyYn, _evtSsnCd, (uwr) =>
        {
            Api1Response apiResponse = JsonUtility.FromJson<Api1Response>(uwr.downloadHandler.text);
            if (apiResponse.CODE == "SUCCESS")
            {
                _pkgOrgStdyTracSno = apiResponse.RESULT.pkgOrgStdyTracSno;
                _attendDtlId = apiResponse.RESULT.attendDtlId;
            }
            callback();
        });
    }


    public void API2(Action callback = null)
    {
        webManager.Get_2RankingCrypt("GET", _baseUrl, _pkgOrgStdyTracSno.ToString(), _attendDtlId.ToString(), score, (uwr) =>
        {
            Api2Response apiResponse = JsonUtility.FromJson<Api2Response>(uwr.downloadHandler.text);
            if (apiResponse.CODE == "SUCCESS")
            {
                _PTS_PRS_PRD = apiResponse.RESULT;
            }
            callback();
        });
    }

    public void API3(Action callback = null)
    {
        SetEndDate();
        webManager.Get_3GameHistory_Save("GET", _baseUrl, _pkgOrgStdyTracSno, _rscSno, _gameType, Stage, _PTS_PRS_PRD, _divType, _startDate, _endDate, _evtSsnCd, (uwr) =>
        {
            callback();
        });
    }

    public void API4()
    {
        webManager.Get_4GameHistory_Update("GET", _baseUrl, _pkgOrgStdyTracSno, _attendDtlId, _rscSno, _historyYn, (uwr) =>
        {
        });
    }

    public void API5(Action callback)
    {
        webManager.Get_5RankingList("GET", _baseUrl, _rscSno, _gameType, stage, (uwr) =>
        {
            Api5Response apiResponse = JsonUtility.FromJson<Api5Response>(uwr.downloadHandler.text);
            if (apiResponse.CODE == "SUCCESS")
            {
                foreach (var gameRecord in apiResponse.LIST)
                {
                    rankingPanel.rankings[gameRecord.USER_RANK - 1].name.text = gameRecord.USER_NAME;
                    rankingPanel.rankings[gameRecord.USER_RANK - 1].score.text = gameRecord.BEST_RECORD;
                }
            }
            callback();
        });
    }

    public void API6()
    {
        webManager.Get_6RankingList("GET", _baseUrl, _pkgOrgStdyTracSno, (uwr) =>
        {

        });
    }

    #endregion

    public void SetStartDate()
    {
        _startDate = GetCurrentTimeAsLong();
    }

    public void SetEndDate()
    {
        _endDate = GetCurrentTimeAsLong();
    }

    public static long GetCurrentTimeAsLong()
    {
        // 현재 시간을 가져옵니다.
        DateTime now = DateTime.Now;

        // 현재 시간을 'yyyymmddHHmmss' 형식의 문자열로 변환합니다.
        string formattedTime = now.ToString("yyyyMMddHHmmss");

        // 문자열을 long 타입으로 파싱합니다.
        long timeAsLong = long.Parse(formattedTime);

        return timeAsLong;
    }

    private void OnApplicationQuit()
    {
        API4();
    }





}
