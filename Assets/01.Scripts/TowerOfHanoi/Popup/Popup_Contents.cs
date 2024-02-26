using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Popup_Contents : MonoBehaviour
{
    [SerializeField]
    int contentNumber;

    [SerializeField]
    public List<Image> contents = new List<Image>();

    [SerializeField]
    RectTransform contentRect;
    float y;


    private List<Vector2> contentsRectTr = new List<Vector2>();
    private RectTransform RectTr;

    [SerializeField]
    private Arrow LeftArrow;
    [SerializeField]
    private Arrow RightArrow;

    [SerializeField]
    private DotManager dotManager;

    [HideInInspector]
    public AudioSource audi;

    [Space(10)]
    [Header("Control")]
    [SerializeField]
    private float SlideTime;
    [SerializeField]
    private float FadeInTime;
    [SerializeField]
    private float FadeOutTime;

    [SerializeField, Range(0f, 1f)]
    private float FadeAlpha;

    private void Awake()
    {
        audi = GetComponent<AudioSource>();
        RectTr = GetComponent<RectTransform>();

        if (contentRect != null)
            y = contentRect.anchoredPosition.y;

        Init();

        //ResetContent();

    }

    public void Init()
    {
        contents.Clear();
        contentsRectTr.Clear();

        // 현재 GameObject의 바로 아래 자식 오브젝트들을 순회
        foreach (Transform child in transform)
        {
            // 각 자식 오브젝트에서 Image 컴포넌트를 가져옴
            Image image = child.GetComponent<Image>();

            // Image 컴포넌트가 있는 경우, 해당 컴포넌트를 사용
            if (image != null)
            {
                // 예: Image 컴포넌트가 있는 자식 오브젝트를 찾았을 때의 처리
                contents.Add(image);
                // 여기서 image 변수를 사용하여 추가 작업을 수행할 수 있음
            }
        }

        // 이미지 가상 트랜스폼 배열
        for (int i = 0; i < contents.Count * 2 - 1; i++)
        {
            contentsRectTr.Add(new Vector2((contents.Count - 1 - i) * -RectTr.rect.width, 0));
        }

        dotManager.Init();

    }

    // 자기 자리 찾아 정렬
    public void ResetContent()
    {
        // 초기 알파값
        for (int i = 0; i < contents.Count; i++)
        {
            Color tempColor = contents[i].GetComponent<Image>().color;
            tempColor.a = FadeAlpha;
            contents[i].GetComponent<Image>().color = tempColor;
        }

        // 처음 이미지 알파값
        Color resetColor = contents[0].GetComponent<Image>().color;
        resetColor.a = 1;
        contents[0].GetComponent<Image>().color = resetColor;

        // 이미지들 배열
        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].transform.localPosition = new Vector2(contentsRectTr[(contentsRectTr.Count - 1) / 2 + i].x, 0);
        }

        contentNumber = 1;

        CheckArrow();
    }

    public void OnClickLeft()
    {
        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].transform.DOLocalMoveX(contentsRectTr[contents.Count - contentNumber + i + 1].x, SlideTime);
        }

        contents[contentNumber - 1].DOFade(FadeAlpha, FadeOutTime);
        contentNumber--;
        //TutoList[tutoNumber - 1].transform.DOLocalMoveX(transform.localPosition.x, SlideTime);
        contents[contentNumber - 1].DOFade(1, FadeInTime);

        CheckArrow();
    }

    public void OnClickRight()
    {
        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].transform.DOLocalMoveX(contentsRectTr[contentsRectTr.Count - contents.Count - contentNumber + i].x, SlideTime);
        }
        contents[contentNumber - 1].DOFade(FadeAlpha, FadeOutTime);
        contentNumber++;
        //TutoList[tutoNumber - 1].transform.DOLocalMoveX(transform.localPosition.x, SlideTime);
        contents[contentNumber - 1].DOFade(1, FadeInTime);

        CheckArrow();
    }

    public void ResetActive()
    {
        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].gameObject.SetActive(false);
        }

        contents[0].gameObject.SetActive(true);

        if (contentRect != null)
        {
            contentRect.anchoredPosition = new Vector3(0, y, 0);
        }

        contentNumber = 1;

        CheckArrow();
    }


    public void OnClickLeftActive()
    {
        contents[contentNumber - 1].gameObject.SetActive(false);
        contentNumber--;
        contents[contentNumber - 1].gameObject.SetActive(true);

        CheckArrow();
    }

    public void OnClickRightActive()
    {
        contents[contentNumber - 1].gameObject.SetActive(false);
        contentNumber++;
        contents[contentNumber - 1].gameObject.SetActive(true);

        CheckArrow();
    }

    public void CheckArrow()
    {
        dotManager.CheckDot(contentNumber);

        if (contentNumber == 1)
        {
            LeftArrow.PointerExit();
            LeftArrow.gameObject.SetActive(false);
            RightArrow.gameObject.SetActive(true);
        }
        else if (contentNumber == contents.Count)
        {
            RightArrow.PointerExit();
            LeftArrow.gameObject.SetActive(true);
            RightArrow.gameObject.SetActive(false);
        }
        else
        {
            LeftArrow.gameObject.SetActive(true); RightArrow.gameObject.SetActive(true);
        }
    }
}
