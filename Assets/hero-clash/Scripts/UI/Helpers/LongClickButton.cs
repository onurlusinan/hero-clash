using HeroClash.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeroClash.UserInterface
{
    /// <summary>
    /// Handles the click & hold function of all hero-related buttons
    /// </summary>
    public class LongClickButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,
                                                   IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private bool _pointerDown;
        private float _pointerDownTimer;
        private bool _shortClick;
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

        private void Update()
        {
            if (!_input)
                return;

            if (_pointerDown)
            {
                _pointerDownTimer += Time.deltaTime;
                if (_pointerDownTimer > requiredHoldTime)
                {
                    _shortClick = false;
                    onLongClick?.Invoke();
                }
                else
                    _shortClick = true;
            }
        }

        public void SetInput(bool input) => _input = input;

        private void Reset()
        {
            _pointerDown = false;
            _shortClick = false;
            _pointerDownTimer = 0f;

            onReleaseLongClick?.Invoke();
        }


        #region INTERFACES
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

            if (!eventData.dragging && _shortClick)
            {
                SoundManager.Instance.Play(Sounds.select);
                onClick?.Invoke();
            }

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
        #endregion
    }
}