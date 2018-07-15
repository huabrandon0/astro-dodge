using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidRage.Events
{
    public abstract class GameEvent1Arg<T>: ScriptableObject
    {
        class GameEvent1ArgEvent : UnityEvent<T> { }

        GameEvent1ArgEvent _event = new GameEvent1ArgEvent();

        public void AddListener(UnityAction<T> listener)
        {
            _event.AddListener(listener);
        }

        public void RemoveListener(UnityAction<T> listener)
        {
            _event.RemoveListener(listener);
        }

        public void Invoke(T arg1)
        {
            _event.Invoke(arg1);
        }

        void OnDisable()
        {
            _event.RemoveAllListeners();
        }
    }
}
