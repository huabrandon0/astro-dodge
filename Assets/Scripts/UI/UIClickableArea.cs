using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AsteroidRage.Events;

namespace AsteroidRage.UI
{
    public class UIClickableArea : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        [SerializeField] private GameEvent _onAreaBeingClickedOn;
        [SerializeField] private GameEvent _onAreaBeingClickedOff;
        
        private bool _isPressed = false;
        private int _pointerId = int.MaxValue;

        bool _enabled = true;

        public void OnPointerDown(PointerEventData data)
        {
            if (!_enabled)
                return;

            if (!_isPressed)
            {
                _isPressed = true;
                _pointerId = data.pointerId;

                if (_onAreaBeingClickedOn)
                    _onAreaBeingClickedOn.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData data)
        {
            if (!_enabled)
                return;

            if (_isPressed && data.pointerId == _pointerId)
            {
                Unpress();
            }
        }

        public void OnPointerUp(PointerEventData data)
        {
            if (!_enabled)
                return;

            if (_isPressed && data.pointerId == _pointerId)
            {
                Unpress();
            }
        }

        void Unpress()
        {
            _isPressed = false;
            _pointerId = int.MaxValue;

            if (_onAreaBeingClickedOff)
                _onAreaBeingClickedOff.Invoke();
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            Unpress();
            _enabled = false;
        }

        public void Toggle()
        {
            if (enabled)
                Disable();
            else
                Enable();
        }
    }
}
