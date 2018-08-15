using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidRage.Events
{
    public class GameEventsListener : MonoBehaviour
    {
        [SerializeField] GameEvent[] _gameEvents;
        [SerializeField] UnityEvent _response;

        void OnEnable()
        {
            foreach (GameEvent gameEvent in _gameEvents)
            {
                gameEvent.AddListener(_response.Invoke);
            }
        }

        void OnDisable()
        {
            foreach (GameEvent gameEvent in _gameEvents)
            {
                gameEvent.RemoveListener(_response.Invoke);
            }
        }
    }
}
