﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidRage.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private UnityEvent _response;

        void OnEnable()
        {
            _gameEvent.AddListener(_response.Invoke);
        }

        void OnDisable()
        {
            _gameEvent.RemoveListener(_response.Invoke);
        }
    }
}