using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    public static WebManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {

        //SendPostRequest("ranking", res, (uwr) =>
        //{
        //    Debug.Log("TODO : UI 갱신하기");
        //});

    }
    //public void SendPostRequest(string url, object obj, Action<UnityWebRequest> callback)
    //{
    //    StartCoroutine(CoSendWebRequest(url, "POST", obj, callback));
    //}

    public void Get_1StudyHistory(string method, string url, int _rscSno, string _historyYn, string _evtSsnCd, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendGet_1StudyHistory(method, url, _rscSno, _historyYn, _evtSsnCd, callback));
    }

    IEnumerator CoSendGet_1StudyHistory(string method, string url, int _rscSno, string _historyYn, string _evtSsnCd, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{url}/innovativelrms/study/afi/insertStudyTrac.do?rscSno={_rscSno}&evtSsnCd={_evtSsnCd}&historyYn={_historyYn}";

        byte[] jsonBytes = null;

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("API1 " + uwr.downloadHandler.text);
            callback?.Invoke(uwr);
        }
    }

    public void Get_2RankingCrypt(string method, string url, string _pkgOrgStdyTracSno, string _attendDtlId, string _score, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendGet_2RankingCrypt(method, url, EncodeToBase64(_pkgOrgStdyTracSno), EncodeToBase64(_attendDtlId), EncodeToBase64(_score), callback));
        print(_score);
        print(EncodeToBase64(_score));
        print(DecodeFromBase64(EncodeToBase64(_score)));
    }


    IEnumerator CoSendGet_2RankingCrypt(string method, string url, string pts, string prs, string prd, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{url}/resourceAjax/cryptoGameRecord?PTS={pts}&PRS={prs}&PRD={prd}";
        byte[] jsonBytes = null;

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("API2  " + uwr.downloadHandler.text);
            callback?.Invoke(uwr);
        }
    }

    public static string EncodeToBase64(string value)
    {
        // 문자열을 바이트 배열로 변환
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(value);

        // 바이트 배열을 Base64 문자열로 인코딩
        string encodedText = Convert.ToBase64String(bytesToEncode);

        return encodedText;
    }

    public static string DecodeFromBase64(string encodedValue)
    {
        // Base64 문자열을 바이트 배열로 디코딩
        byte[] decodedBytes = Convert.FromBase64String(encodedValue);

        // 바이트 배열을 문자열로 변환
        string decodedText = Encoding.UTF8.GetString(decodedBytes);

        return decodedText;
    }

    public void Get_3GameHistory_Save(string method, string url, int _pkgOrgStdyTracSno, int _rscSno, string _gameType, int _stageStep, string _score, string _divType, long _startDate, long _endDate, string _evtSno, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendGet_3GameHistory_Save(method, url, _pkgOrgStdyTracSno, _rscSno, _gameType, _stageStep, _score, _divType, _startDate, _endDate, _evtSno, callback));
    }

    IEnumerator CoSendGet_3GameHistory_Save(string method, string url, int _pkgOrgStdyTracSno, int _rscSno, string _gameType, int _stageStep, string prd, string _divType, long _startDate, long _endDate, string _evtSno, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{url}/resourceAjax/saveGameHist?TRAC_SNO={_pkgOrgStdyTracSno}&RSC_SNO={_rscSno}&GAME_TYPE={_gameType}&STAGE_STEP={_stageStep}&RECORD={prd}&DEVICE_TYPE={_divType}&START_DATE={_startDate}&END_DATE={_endDate}&EVT_SNO=";
        byte[] jsonBytes = null;

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("API3  " + uwr.downloadHandler.text);
            callback?.Invoke(uwr);
        }
    }

    public void Get_4GameHistory_Update(string method, string url, int _pkgOrgStdyTracSno, int _attendDtlId, int _rscSno, string _historyYn, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendGet_4GameHistory_Update(method, url, _pkgOrgStdyTracSno, _attendDtlId, _rscSno, _historyYn, callback));
    }

    IEnumerator CoSendGet_4GameHistory_Update(string method, string url, int _pkgOrgStdyTracSno, int _attendDtlId, int _rscSno, string _historyYn, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{url}/innovativelrms/study/afi/updateStudyTrac.do?rscSno={_rscSno}&pkgOrgStdyTracSno={_pkgOrgStdyTracSno}&attendDtlId={_attendDtlId}&historyYn={_historyYn}";
        byte[] jsonBytes = null;

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("API4  " + uwr.downloadHandler.text);
            callback?.Invoke(uwr);
        }
    }

    public void Get_5RankingList(string method, string url, int _rscSno, string _gameType, int _stageStep, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendGet_5RankingList(method, url, _rscSno, _gameType, _stageStep, callback));
    }

    IEnumerator CoSendGet_5RankingList(string method, string url, int _rscSno, string _gameType, int _stageStep, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{url}/resourceAjax/findGameRankingList?RSC_SNO={_rscSno}&GAME_TYPE={_gameType}&STAGE_STEP={_stageStep}";

        byte[] jsonBytes = null;

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("API5  " + uwr.downloadHandler.text);
            callback?.Invoke(uwr);
        }
    }

    public void Get_6RankingList(string method, string url, int _pkgOrgStdyTracSno, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendGet_6RankingList(method, url, _pkgOrgStdyTracSno, callback));
    }

    IEnumerator CoSendGet_6RankingList(string method, string url, int _pkgOrgStdyTracSno, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{url}/resourceAjax/findGameStage?TRAC_SNO=${_pkgOrgStdyTracSno}";

        byte[] jsonBytes = null;

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("API6  " + uwr.downloadHandler.text);
            callback?.Invoke(uwr);
        }
    }

}
