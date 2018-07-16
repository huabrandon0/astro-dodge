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
        
        private bool _isPressed = false;
        private int _pointerId = int.MaxValue;

        public void OnPointerDown(PointerEventData data)
        {
            if (!_isPressed)
            {
                _isPressed = true;
                _pointerId = data.pointerId;
                _onAreaBeingClickedOn.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData data)
        {
            if (_isPressed && data.pointerId == _pointerId)
            {
                _isPressed = false;
                _pointerId = int.MaxValue;
            }
        }

        public void OnPointerUp(PointerEventData data)
        {
            if (_isPressed && data.pointerId == _pointerId)
            {
                _isPressed = false;
                _pointerId = int.MaxValue;
            }
        }
    }
}
