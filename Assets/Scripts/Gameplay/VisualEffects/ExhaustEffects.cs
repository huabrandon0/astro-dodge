using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using EZCameraShake;

namespace AsteroidRage.Game
{
    public class ExhaustEffects : MonoBehaviour
    {
        [SerializeField] ParticleSystem[] _exhausts;
        [SerializeField] ParticleSystem[] _megaExhausts;
        [SerializeField] ParticleSystem[] _flares;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent PlayerFlaredEvent;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        CameraShakeInstance _playerCameraShakeInstance;
        CameraShakeInstance _warpDriveParticleCameraShakeInstance;

        public void Flare()
        {
            _invokeEvents.PlayerFlaredEvent.Invoke();

            foreach (ParticleSystem ps in _flares)
                ps.Play();
        }

        public void MegaFlareOn()
        {
            _playerCameraShakeInstance = CameraShaker.GetInstance("PlayerCamera").StartShake(.3f, 3.5f, .5f);
            _warpDriveParticleCameraShakeInstance = CameraShaker.GetInstance("WarpDriveParticleCamera").StartShake(.3f, 3.5f, .5f);

            foreach (ParticleSystem ps in _megaExhausts)
                ps.Play();

            foreach (ParticleSystem ps in _exhausts)
                ps.Stop();
        }

        public void MegaFlareOff()
        {
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
    }
}
