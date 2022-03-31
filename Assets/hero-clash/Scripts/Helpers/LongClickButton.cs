using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour,  IPointerUpHandler, IPointerDownHandler,
                                               IDragHandler, IBeginDragHandler, IEndDragHandler,
                                               IPointerEnterHandler, IPointerExitHandler
{
    private bool _pointerDown;
    private float _pointerDownTimer;
    private bool _pointerDrag;
    private bool _pointerHover;

    private ScrollRect _scrollRectParent;

    [Header("Click Config")]
    public float requiredHoldTime;
    public Hero hero;

    public UnityEvent onLongClick;
    public UnityEvent onReleaseLongClick;

    public void Awake()
    {
        if (GetComponentInParent<ScrollRect>() != null)
            _scrollRectParent = GetComponentInParent<ScrollRect>();
    }

    public delegate void ClickHandler();
    public static event ClickHandler OnClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown was called for object " + gameObject.name);
        _pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp was called for object " + gameObject.name);

        if (_pointerHover && OnClick != null)
            OnClick();

        if(!eventData.dragging)
            hero.HeroCardPressed();

        Reset();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_scrollRectParent != null)
            _scrollRectParent.OnDrag(eventData);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_scrollRectParent != null)
            _scrollRectParent.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_scrollRectParent != null)
            _scrollRectParent.OnEndDrag(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerHover = false;
    }

    private void Reset()
    {
        _pointerDown = false;
        _pointerDrag = false;
        _pointerDownTimer = 0f;

        onReleaseLongClick?.Invoke();
    }

    private void Update()
    {
        if (_pointerDown)
        {
            _pointerDownTimer += Time.deltaTime;
            if (_pointerDownTimer > requiredHoldTime)
            {
                onLongClick?.Invoke();  
            }
        }
    }
}
