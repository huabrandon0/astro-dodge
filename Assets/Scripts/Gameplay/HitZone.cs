using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class HitZone : MonoBehaviour
    {
        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent RowUnitExitedHitZone;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RowUnit"))
            {
                _invokeEvents.RowUnitExitedHitZone.Invoke();
            }
        }
    }
}
