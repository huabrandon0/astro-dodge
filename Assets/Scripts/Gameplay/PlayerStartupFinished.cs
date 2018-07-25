using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

public class PlayerStartupFinished : MonoBehaviour
{
    [System.Serializable]
    public class InvokeEvents
    {
        public GameEvent PlayerStartupFinished;
    }

    [SerializeField] InvokeEvents _invokeEvents;

    public void InvokePlayerStartupFinished()
    {
        _invokeEvents.PlayerStartupFinished.Invoke();
    }
}
