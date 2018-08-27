using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using EZCameraShake;

namespace AsteroidRage.Game
{
    public class ModelExhaustEffects : MonoBehaviour
    {
        [SerializeField] ParticleSystem[] _exhausts;
        [SerializeField] ParticleSystem[] _flares;
        [SerializeField] ParticleSystem[] _megaFlares;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent ModelFlaredEvent;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        public void Exhaust()
        {
            foreach (ParticleSystem ps in _exhausts)
            {
                ps.Play();
            }
        }

        public void Flare()
        {
            _invokeEvents.ModelFlaredEvent.Invoke();

            foreach (ParticleSystem ps in _flares)
            {
                ps.Play();
            }
        }

        public void MegaFlareOn()
        {
            _invokeEvents.ModelFlaredEvent.Invoke();

            foreach (ParticleSystem ps in _megaFlares)
            {
                ps.Play();
            }

            foreach (ParticleSystem ps in _exhausts)
            {
                ps.Stop();
            }
        }

        public void MegaFlareOff()
        {
            foreach (ParticleSystem ps in _megaFlares)
            {
                ps.Stop();
            }
        }
    }
}
