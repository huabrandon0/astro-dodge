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

        Rigidbody _rb;

        [SerializeField] Material _offMaterial;
        Material _onMaterial;

        MeshRenderer _mr;

        Collider _col;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _mr = GetComponentInChildren<MeshRenderer>();
            _col = GetComponent<Collider>();
            _onMaterial = _mr.material;

            _responseEvents.CountChanged.AddListener(ScaleUp);
        }

        public void ScaleUp(int count)
        {
            if (enabled && _rb.velocity.magnitude != 0f)
            {
                float scale = 1f + _diffConfig.VelocityScaleStep * Mathf.Round(count / _diffConfig.VelocityScaleInterval);
                _rb.velocity = _rb.velocity.normalized * _diffConfig.StartSpeed * scale;
            }
        }

        void OnEnable()
        {
            _mr.material = _onMaterial;
            _col.enabled = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("HitZone"))
            {
                _mr.material = _offMaterial;
                _col.enabled = false;
            }
        }
    }
}
