using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Events
{
    public class GameEventInvoker : MonoBehaviour
    {
        [SerializeField] GameEvent _gameEvent;

        public void InvokeGameEvent()
        {
            _gameEvent.Invoke();
        }
    }
}
