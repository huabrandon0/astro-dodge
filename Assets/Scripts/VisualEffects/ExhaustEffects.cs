using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using EZCameraShake;

namespace AsteroidRage.Game
{
    public class ExhaustEffects : MonoBehaviour
    {
        [SerializeField] ParticleSystem[] _flares;
        [SerializeField] ParticleSystem[] _megaFlares;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent PlayerFlaredEvent;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        CameraShakeInstance _playerCameraShakeInstance;
        CameraShakeInstance _warpDriveParticleCameraShakeInstance;

        //ParticleSystem[] _particleSystems;

        //[SerializeField] float _flareScale = 3f;
        //[SerializeField] Color _flareColor;

        //void Awake()
        //{
        //    _particleSystems = GetComponentsInChildren<ParticleSystem>();
        //}

        //public void Flare()
        //{
        //    foreach (ParticleSystem ps in _particleSystems)
        //    {
        //        StartCoroutine(Flare(ps.main));
        //    }
        //}

        //IEnumerator Flare(ParticleSystem.MainModule psmain)
        //{
        //    float startSize = psmain.startSize.constant;
        //    float flareSize = startSize * _flareScale;

        //    Color startColor = psmain.startColor.color;

        //    float startTime = Time.time - Time.deltaTime;
        //    float interpolationValue = 0f;
        //    while (interpolationValue <= 1f)
        //    {
        //        interpolationValue = (Time.time - startTime);
        //        psmain.startSize = Mathf.Lerp(flareSize, startSize, interpolationValue);
        //        psmain.startColor = Color.Lerp(_flareColor, startColor, interpolationValue);
        //        yield return null;
        //    }
        //}

        public void Flare()
        {
            _invokeEvents.PlayerFlaredEvent.Invoke();

            foreach (ParticleSystem ps in _flares)
            {
                ps.Play();
            }
        }

        public void MegaFlareOn()
        {
            _playerCameraShakeInstance = CameraShaker.GetInstance("PlayerCamera").StartShake(.3f, 3.5f, .5f);
            _warpDriveParticleCameraShakeInstance = CameraShaker.GetInstance("WarpDriveParticleCamera").StartShake(.3f, 3.5f, .5f);

            foreach (ParticleSystem ps in _megaFlares)
            {
                ps.Play();
            }
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

            foreach (ParticleSystem ps in _megaFlares)
            {
                ps.Stop();
            }
        }
    }
}
