using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.UI
{
    public class StartupScreen : MonoBehaviour
    {
        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent GameLoadedEvent;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        void Start()
        {
            _invokeEvents.GameLoadedEvent.Invoke();
        }
    }
}
