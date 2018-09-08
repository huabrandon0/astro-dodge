using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TWM.UI
{
    public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        [SerializeField] UnityEvent _onButtonDown;
        [SerializeField] UnityEvent _onButtonUp;
        [SerializeField] UnityEvent _onButtonExit;

        bool _isPressed = false;
        int _pointerId = int.MaxValue;

        bool _enabled = true;

        UIElementAnimator _imageAnimator;

        void Awake()
        {
            _imageAnimator = GetComponentInChildren<UIElementAnimator>();
        }

        public void OnPointerDown(PointerEventData data)
        {
            if (!_enabled)
                return;

            if (!_isPressed)
            {
                Press(data.pointerId);

                if (_onButtonDown != null)
                    _onButtonDown.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData data)
        {
            if (_isPressed && data.pointerId == _pointerId)
            {
                Unpress();
                if (_onButtonExit != null)
                    _onButtonExit.Invoke();
            }
        }

        public void OnPointerUp(PointerEventData data)
        {
            if (_isPressed && data.pointerId == _pointerId)
            {
                Unpress();
                if (_onButtonUp != null)
                    _onButtonUp.Invoke();
            }
        }

        void Press(int pointerId)
        {
            _isPressed = true;
            _pointerId = pointerId;
            if (_imageAnimator)
                _imageAnimator.PushOut();
        }

        void Unpress()
        {
            _isPressed = false;
            _pointerId = int.MaxValue;
            if (_imageAnimator)
                _imageAnimator.PushIn();
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            if (_isPressed)
                Unpress();

            _enabled = false;
        }

        public void Toggle()
        {
            if (_enabled)
                Disable();
            else
                Enable();
        }
    }
}
