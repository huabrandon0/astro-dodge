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

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent PlayerHit;
            public GameEvent ShieldHit;
            public GameEventInt CurrencyCollected;
        }

        [SerializeField] InvokeEvents _invokeEvents;
        
        [SerializeField] DifficultyConfig _diffConfig;

        Rigidbody _rb;
        ParticleSystem _explosion;
        AsteroidModel _asteroidModel;

        public bool _canHitPlayer = true;

        int _currencyValue = 0;
        [SerializeField] int _minCurrencyValue = 1;
        [SerializeField] int _maxCurrencyValue = 4;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _explosion = GetComponentInChildren<ParticleSystem>();
            _asteroidModel = GetComponentInChildren<AsteroidModel>();
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

        void OnEnable()
        {
            _currencyValue = Random.Range(_minCurrencyValue, _maxCurrencyValue);
            _canHitPlayer = true;

            if (_asteroidModel)
                _asteroidModel.On();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_canHitPlayer)
                    _invokeEvents.PlayerHit.Invoke();
            }
            else if (other.CompareTag("HitZone"))
            {
                var em = _explosion.emission;
                em.SetBurst(0, new ParticleSystem.Burst(0f, _currencyValue));
                _explosion.Play();
                _canHitPlayer = false;
                _invokeEvents.CurrencyCollected.Invoke(_currencyValue);

                if (_asteroidModel)
                    _asteroidModel.Off();
            }
        }
    }
}
