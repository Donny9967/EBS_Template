using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [System.Serializable]
    private class OnPointerEnterHandler : UnityEvent { }
    [System.Serializable]
    private class OnPointerExitHandler : UnityEvent { }
    [System.Serializable]
    private class OnClickEvent : UnityEvent { }

    // Text UI를 클릭했을 때 호출하고 싶은 메소드 등록
    [SerializeField]
    private OnPointerEnterHandler pointerEnterHandler;
    [SerializeField]
    private OnPointerExitHandler pointerExitHandler;
    [SerializeField]
    private OnClickEvent onClickEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnterHandler?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerExitHandler?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
    }
}
