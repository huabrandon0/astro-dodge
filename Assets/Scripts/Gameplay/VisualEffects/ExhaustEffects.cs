using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using EZCameraShake;
using System.Linq;

namespace AsteroidRage.Game
{
    public class ExhaustEffects : MonoBehaviour
    {
        List<ParticleSystem> _exhausts;
        List<ParticleSystem> _megaExhausts;
        List<ParticleSystem> _flares;
        List<StandingTrail> _trails;

        [SerializeField] string _exhaustName = "Exhaust";
        [SerializeField] string _megaExhaustName = "MegaExhaust";
        [SerializeField] string _flareName = "Flare";

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent PlayerFlaredEvent;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        CameraShakeInstance _playerCameraShakeInstance;
        CameraShakeInstance _warpDriveParticleCameraShakeInstance;

        bool _exhaustOn = true;

        void Awake()
        {
            _exhausts = new List<ParticleSystem>();
            _megaExhausts = new List<ParticleSystem>();
            _flares = new List<ParticleSystem>();
            _trails = GetComponentsInChildren<StandingTrail>().ToList();

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

            foreach (ParticleSystem ps in _exhausts)
            {
                ParticleSystem.MainModule main = ps.main;
                main.playOnAwake = _exhaustOn;
            }
        }

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
            if (!isActiveAndEnabled)
                return;

            _invokeEvents.PlayerFlaredEvent.Invoke();

            foreach (ParticleSystem ps in _flares)
                ps.Play();
        }

        public void MegaExhaustOff()
        {
            if (!isActiveAndEnabled)
                return;

            if (_playerCameraShakeInstance != null)
            {
                _playerCameraShakeInstance.StartFadeOut(1f);
                _playerCameraShakeInstance = null;
            }

            if (_warpDriveParticleCameraShakeInstance != null)
            {
                _warpDriveParticleCameraShakeInstance.StartFadeOut(1f);
                _warpDriveParticleCameraShakeInstance = null;
            }

            foreach (ParticleSystem ps in _megaExhausts)
                ps.Stop();

            foreach (ParticleSystem ps in _exhausts)
                ps.Play();
        }

        public void MegaExhaustOn()
        {
            if (!isActiveAndEnabled)
                return;

            _playerCameraShakeInstance = CameraShaker.GetInstance("PlayerCamera").StartShake(.3f, 3.5f, .5f);
            _warpDriveParticleCameraShakeInstance = CameraShaker.GetInstance("WarpDriveParticleCamera").StartShake(.3f, 3.5f, .5f);

            foreach (ParticleSystem ps in _megaExhausts)
                ps.Play();

            foreach (ParticleSystem ps in _exhausts)
                ps.Stop();
        }

        public void TrailOn()
        {
            if (!isActiveAndEnabled)
                return;

            foreach (StandingTrail trail in _trails)
                trail.On();
        }

        public void TrailOff()
        {
            if (!isActiveAndEnabled)
                return;

            foreach (StandingTrail trail in _trails)
                trail.Off();
        }

        public void SetTrailSpeed(float speed)
        {
            if (!isActiveAndEnabled)
                return;

            foreach (StandingTrail trail in _trails)
                trail._speed = speed;
        }
    }
}
