using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : InteractiveUI
{
    private Image Img;

    public Popup_Contents Content;

    public Sprite EnterSprite;
    public Sprite ExitSprite;
    public AudioClip Clip_SelectButton;

    public bool isLeft;

    private void Awake()
    {
        Img = GetComponent<Image>();
    }

    public override void PointerEnter()
    {
        if (EnterSprite != null)
        {
            Img.sprite = EnterSprite;
        }
    }

    public override void PointerExit()
    {
        if (ExitSprite != null)
        {
            Img.sprite = ExitSprite;
        }
    }

    public override void OnClick()
    {
        Content.audi.PlayOneShot(Clip_SelectButton);

        if (isLeft)
        {
            //Content.OnClickLeft();
            Content.OnClickLeftActive();
        }
        else
        {
            //Content.OnClickRight();
            Content.OnClickRightActive();
        }
    }
}
