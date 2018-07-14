using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AsteroidRage.Events;

namespace AsteroidRage.UI
{
    public class UIClickableArea : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameEvent _onAreaClickedEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            _onAreaClickedEvent.Invoke();
        }
    }
}
