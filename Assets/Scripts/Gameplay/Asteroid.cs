using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class Asteroid : PooledMonobehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt CountChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;
        
        [SerializeField] DifficultyConfig _diffConfig;

        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            _responseEvents.CountChanged.AddListener(ScaleUp);
        }

        public void ScaleUp(int count)
        {
            if (enabled && rb.velocity.magnitude != 0f)
            {
                float scale = 1f + _diffConfig.VelocityScaleStep * Mathf.Round(count / _diffConfig.VelocityScaleInterval);
                rb.velocity = rb.velocity.normalized * _diffConfig.StartSpeed * scale;
            }
        }
    }
}
