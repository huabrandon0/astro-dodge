using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AsteroidRage.Events;

namespace AsteroidRage.UI
{
    public class UIMultiFingerClickableArea : MonoBehaviour, IPointerDownHandler
    {
        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent OneTap;
            public GameEvent TwoTap;
            public GameEvent ThreeTap;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        public float _pollingPeriod = 0.05f;

        public float _bufferPeriod = 0.05f;

        bool _isPolling = false;

        List<int> _pointerIds;

        void Awake()
        {
            _pointerIds = new List<int>();
        }

        void Update()
        {

        }

        public void OnPointerDown(PointerEventData data)
        {
            if (!_isPolling)
            {
                StartCoroutine(Poll(_pollingPeriod));
                _pointerIds.Clear();
            }
            
            _pointerIds.Add(data.pointerId);
        }

        IEnumerator Poll(float time)
        {
            _isPolling = true;
            yield return new WaitForSeconds(time); // skip this time if pointer up event?

            int numberOfTaps = Mathf.Min(_pointerIds.Count, 3);
            switch (numberOfTaps)
            {
                case 1:
                    StartCoroutine(BufferGameEvent(_invokeEvents.OneTap, _bufferPeriod));
                    break;
                case 2:
                    StartCoroutine(BufferGameEvent(_invokeEvents.TwoTap, _bufferPeriod));
                    break;
                case 3:
                    StartCoroutine(BufferGameEvent(_invokeEvents.ThreeTap, _bufferPeriod));
                    break;
                default:
                    break;
            }

            _isPolling = false;
        }

        IEnumerator BufferGameEvent(GameEvent gameEvent, float bufferPeriod)
        {
            float startTime = Time.time;
            while (Time.time - startTime < bufferPeriod)
            {
                gameEvent.Invoke();
                yield return null;
            }
        }
    }
}
