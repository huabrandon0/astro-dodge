using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class PlayerHit : MonoBehaviour
    {
        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent PlayerHit;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Asteroid"))
            {
                Asteroid ast = other.GetComponent<Asteroid>();
                if (ast && ast._canHitPlayer)
                {
                    _invokeEvents.PlayerHit.Invoke();
                }
            }
        }
    }
}