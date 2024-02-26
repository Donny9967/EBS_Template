using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputName : Popup
{
    [SerializeField]
    TextMeshProUGUI PlaceHolder;
    [SerializeField]
    public TMP_InputField PlayerName;

    public string RandName;
    string[] randName = { "으뜸", "버들", "노을", "나리", "하루", "나비", "아롱", "바다", "한별", "샛별", "소리", "사랑", "마음", "음샘", "슬기", "이상", "오현", "백은", "한란", "이경", "나영", "가영", "병헌", "이수", "성한", "유현", "박호", "이레", "보다", "들레" };

    public override void Init()
    {
        GetName_Random();
    }

    public void OnClickOK()
    {
        if (PlayerName.text == "")
        {
            popupManager.NameConfirm(RandName, Number);
            return;
        }

        popupManager.NameConfirm(PlayerName.text, Number);

    }

    public void GetName_Random()
    {
        int Intname = Random.Range(0, randName.Length);
        int value = Random.Range(0, 100);

        string randText;

        if (value < 10)
            randText = randName[Intname] + "0" + value.ToString();
        else
            randText = randName[Intname] + value.ToString();

        PlaceHolder.text = randText;
        RandName = randText;

        PlayerName.text = "";
    }
}
