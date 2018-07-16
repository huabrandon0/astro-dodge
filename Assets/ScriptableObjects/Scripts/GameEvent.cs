using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidRage.Events
{
    [CreateAssetMenu(menuName = "Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private UnityEvent _unityEvent = new UnityEvent();

        public void AddListener(UnityAction listener)
        {
            _unityEvent.AddListener(listener);
        }

        public void RemoveListener(UnityAction listener)
        {
            _unityEvent.RemoveListener(listener);
        }

        public void Invoke()
        {
            _unityEvent.Invoke();
        }

        void OnDisable()
        {
            _unityEvent.RemoveAllListeners();
        }
    }
}
