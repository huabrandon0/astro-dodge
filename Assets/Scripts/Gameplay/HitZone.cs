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
            public GameEvent SpecialAsteroidExploded;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RowUnit"))
            {
                RowUnit rowUnit = other.GetComponent<RowUnit>();
                if (rowUnit != null)
                {
                    _invokeEvents.RowUnitExitedHitZone.Invoke();

                    if (rowUnit.containsSpecial)
                        _invokeEvents.SpecialAsteroidExploded.Invoke();

                    //if (rowUnit.containsGoldenAsteroids)
                    //    _invokeEvents.GoldenAsteroidExploded.Invoke();

                    //if (rowUnit.containsDiamondAsteroids)
                    //    _invokeEvents.DiamondAsteroidExploded.Invoke();
                }
            }
        }
    }
}
