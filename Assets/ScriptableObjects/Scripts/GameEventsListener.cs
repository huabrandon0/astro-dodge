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

        [System.Serializable]
        class ResponseWithDelay
        {
            public UnityEvent Response;
            public float Delay;
        }

        [SerializeField] ResponseWithDelay[] _responsesWithDelays;

        void OnEnable()
        {
            foreach (GameEvent gameEvent in _gameEvents)
            {
                gameEvent.AddListener(_response.Invoke);

                foreach (ResponseWithDelay rwd in _responsesWithDelays)
                {
                    gameEvent.AddListener(() => Invoke(rwd.Response, rwd.Delay));
                }
            }
        }

        void OnDisable()
        {
            foreach (GameEvent gameEvent in _gameEvents)
            {
                gameEvent.RemoveListener(_response.Invoke);
            }
        }

        void Invoke(UnityEvent unityEvent, float delay)
        {
            StartCoroutine(InvokeAfterSeconds(unityEvent, delay));
        }

        IEnumerator InvokeAfterSeconds(UnityEvent unityEvent, float time)
        {
            yield return new WaitForSeconds(time);
            unityEvent.Invoke();
        }
    }
}
