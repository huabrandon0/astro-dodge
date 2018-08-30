using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class RowUnit : PooledMonobehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt CountChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;
        
        [SerializeField] DifficultyConfig _diffConfig;

        Rigidbody _rb;

        public bool containsAsteroids = false;
        public bool containsGoldenAsteroids = false;
        public bool containsDiamondAsteroids = false;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _responseEvents.CountChanged.AddListener(ScaleUp);
        }

        public void ScaleUp(int count)
        {
            if (enabled && _rb.velocity.magnitude != 0f)
            {
                float desiredScale = 1f + _diffConfig.VelocityScaleStep * Mathf.Round(count / _diffConfig.VelocityScaleInterval);
                float scale = Mathf.Min(desiredScale, _diffConfig.VelocityScaleMax);
                _rb.velocity = _rb.velocity.normalized * _diffConfig.StartSpeed * scale;
            }
        }
    }
}
