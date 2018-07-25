﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

public class PlayerHit : MonoBehaviour
{
    [System.Serializable]
    public class InvokeEvents
    {
        public GameEvent PlayerHit;
        public GameEvent PlayerExploded;
    }

    [SerializeField] InvokeEvents _invokeEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            _invokeEvents.PlayerHit.Invoke();
        }
    }

    public void InvokePlayerExplodedEvent()
    {
        _invokeEvents.PlayerExploded.Invoke();
    }
}
