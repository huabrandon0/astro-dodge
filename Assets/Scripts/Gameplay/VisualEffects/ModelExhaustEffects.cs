using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using EZCameraShake;
using System.Linq;

namespace AsteroidRage.Game
{
    public class ModelExhaustEffects : MonoBehaviour
    {
        List<ParticleSystem> _exhausts;
        List<ParticleSystem> _megaExhausts;
        List<ParticleSystem> _flares;

        [SerializeField] string _exhaustName = "Exhaust";
        [SerializeField] string _megaExhaustName = "MegaExhaust";
        [SerializeField] string _flareName = "Flare";

        bool _exhaustOn = false;

        void Awake()
        {
            _exhausts = new List<ParticleSystem>();
            _megaExhausts = new List<ParticleSystem>();
            _flares = new List<ParticleSystem>();

            List<ParticleSystem> particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                if (particleSystem.name.StartsWith(_exhaustName))
                    _exhausts.Add(particleSystem);
                else if (particleSystem.name.StartsWith(_megaExhaustName))
                    _megaExhausts.Add(particleSystem);
                else if (particleSystem.name.StartsWith(_flareName))
                    _flares.Add(particleSystem);
            }
        }

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent ModelFlaredEvent;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        public void ExhaustOn()
        {
            if (!isActiveAndEnabled)
                return;

            _exhaustOn = true;

            foreach (ParticleSystem ps in _exhausts)
                ps.Play();
        }

        public void ExhaustOff()
        {
            if (!isActiveAndEnabled)
                return;

            _exhaustOn = false;

            foreach (ParticleSystem ps in _exhausts)
                ps.Stop();
        }

        public void Flare()
        {
            _invokeEvents.ModelFlaredEvent.Invoke();

            foreach (ParticleSystem ps in _flares)
                ps.Play();
        }

        public void MegaExhaustOn()
        {
            foreach (ParticleSystem ps in _megaExhausts)
                ps.Play();

            foreach (ParticleSystem ps in _exhausts)
                ps.Stop();
        }

        public void MegaExhaustOff()
        {
            foreach (ParticleSystem ps in _megaExhausts)
                ps.Stop();

            if (_exhaustOn)
            {
                foreach (ParticleSystem ps in _exhausts)
                    ps.Play();
            }
        }
    }
}
