using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class PlayerMegaBoost : MonoBehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEvent StartMegaBoostOnEvent;
        }

        [SerializeField] ResponseEvents _responseEvents;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent MegaBoostStart;
            public GameEvent MegaBoostEnd;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        bool _boost = false;
        bool _canBoost = true;

        void Awake()
        {
            _responseEvents.StartMegaBoostOnEvent.AddListener(ToggleBoost);
        }

        public void EnableBoost()
        {
            _canBoost = true;
        }

        public void DisableBoost()
        {
            _canBoost = false;
            if (_boost)
                EndBoost();
        }

        public void ToggleBoost()
        {
            if (_boost)
                EndBoost();
            else
                StartBoost();
        }

        public void Boost()
        {
            
        }

        void StartBoost()
        {
            if (!_canBoost)
                return;

            _boost = true;
            _invokeEvents.MegaBoostStart.Invoke();
        }

        void EndBoost()
        {
            _boost = false;
            _invokeEvents.MegaBoostEnd.Invoke();
        }
    }
}