using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidRage.Events
{
    public class DelayedGameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private UnityEvent _response;
        [SerializeField] private float delay;

        void OnEnable()
        {
            _gameEvent.AddListener(DelayedInvoke);
        }

        void OnDisable()
        {
            _gameEvent.RemoveListener(DelayedInvoke);
        }

        void DelayedInvoke()
        {
            StartCoroutine(InvokeAfterSeconds(delay));
        }

        IEnumerator InvokeAfterSeconds(float time)
        {
            yield return new WaitForSeconds(time);
            _response.Invoke();
        }
    }
}
