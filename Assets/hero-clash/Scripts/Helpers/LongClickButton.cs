using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _pointerDown;
    private float _pointerDownTimer;

    public float requiredHoldTime;

    public UnityEvent onLongClick;
    public UnityEvent onReleaseLongClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Reset()
    {
        _pointerDown = false;
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
