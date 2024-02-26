using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField]
    public bool onTutorial;

    [Space(20)]
    public bool OnPopup;

    [SerializeField]
    public int Number;

    protected PopupManager popupManager;

    public virtual void Awake()
    {
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        popupManager = GetComponentInParent<PopupManager>();
    }

    public virtual void Init()
    {

    }

    public virtual void OnEnable()
    {

    }



    public virtual void OnPopupActive()
    {

    }

    public virtual void OnClickExit()
    {

    }
}
