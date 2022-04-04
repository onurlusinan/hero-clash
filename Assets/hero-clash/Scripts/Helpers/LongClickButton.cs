using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour,  IPointerUpHandler, IPointerDownHandler,
                                               IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool _pointerDown;
    private float _pointerDownTimer;
    private bool _fakeClick;
    public bool _input;

    private ScrollRect _scrollRectParent;

    [Header("Click Config")]
    public float requiredHoldTime;

    public UnityEvent onLongClick;
    public UnityEvent onReleaseLongClick;
    public UnityEvent onClick;


    public void Awake()
    {
        if (GetComponentInParent<ScrollRect>() != null)
            _scrollRectParent = GetComponentInParent<ScrollRect>();

        SetInput(true);
    }

    public void SetInput(bool input)
    {
        _input = input;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_input)
            return;

        _pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_input)
            return;

        if (!eventData.dragging && _fakeClick)
            onClick?.Invoke();
        
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

    private void Reset()
    {
        _pointerDown = false;
        _fakeClick = false;
        _pointerDownTimer = 0f;

        onReleaseLongClick?.Invoke();
    }

    private void Update()
    {
        if (!_input)
            return;

        if (_pointerDown)
        {
            _pointerDownTimer += Time.deltaTime;
            if (_pointerDownTimer > requiredHoldTime)
            {
                _fakeClick = false;
                onLongClick?.Invoke();
            }
            else
                _fakeClick = true;
        }
    }
}
