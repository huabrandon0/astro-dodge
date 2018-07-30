using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class ExhaustEffects : MonoBehaviour
    {
        ParticleSystem[] _particleSystems;

        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt CountChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;

        [SerializeField] DifficultyConfig _diffConfig;
        
        float _sizeScale = 1f;

        void Awake()
        {
            _particleSystems = GetComponentsInChildren<ParticleSystem>();

            _responseEvents.CountChanged.AddListener(Flare);
        }

        public void Flare(int count)
        {
            float oldSizeScale = _sizeScale;
            _sizeScale = 1f + _diffConfig.MoveSpeedScaleStep * Mathf.Round(count / _diffConfig.MoveSpeedScaleInterval);

            if (_sizeScale != oldSizeScale)
            {
                foreach (ParticleSystem ps in _particleSystems)
                {
                    StartCoroutine(Flare(ps.main));
                }
            }
        }

        IEnumerator Flare(ParticleSystem.MainModule psmain)
        {
            float startSize = psmain.startSize.constant;
            float flareSize = startSize * 2f;

            float startTime = Time.time - Time.deltaTime;
            float interpolationValue = 0f;
            while (interpolationValue <= 1f)
            {
                interpolationValue = (Time.time - startTime);
                psmain.startSize = Mathf.Lerp(flareSize, startSize, interpolationValue);
                yield return null;
            }
        }
    }
}
