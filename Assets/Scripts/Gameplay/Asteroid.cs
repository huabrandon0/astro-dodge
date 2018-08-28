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
            public GameEvent AsteroidExploded;
        }

        [SerializeField] InvokeEvents _invokeEvents;
        
        [SerializeField] DifficultyConfig _diffConfig;

        Rigidbody _rb;
        MeshRenderer _mr;
        Collider _col;

        [SerializeField] Color _onColor;
        [SerializeField] Color _offColor;

        [SerializeField] float _fadeInTime;

        ParticleSystem _explosion;

        public bool _canHitPlayer = true;

        int _currencyValue = 0;
        [SerializeField] int _minCurrencyValue = 1;
        [SerializeField] int _maxCurrencyValue = 4;

        [SerializeField] GameObject[] _asteroidModels;

        void Awake()
        {
            GameObject model = Instantiate(_asteroidModels[Random.Range(0, _asteroidModels.Length)], transform);
            model.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            _rb = GetComponent<Rigidbody>();
            _mr = GetComponentInChildren<MeshRenderer>();
            _col = GetComponentInChildren<Collider>();
            _explosion = GetComponentInChildren<ParticleSystem>();
            
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
            TurnOnFade();
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
                //transform.position = new Vector3(transform.position.x, -0.3f, -0.5f);
                var em = _explosion.emission;
                em.SetBurst(0, new ParticleSystem.Burst(0f, _currencyValue));
                TurnOffExplode();
                _invokeEvents.CurrencyCollected.Invoke(_currencyValue);
            }
            else if (other.CompareTag("Shield"))
            {
                // TODO: Explode asteroid effect
                _canHitPlayer = false;
            }
        }

        public void TurnOn()
        {
            _mr.material.color = new Color(_mr.material.color.r, _mr.material.color.g, _mr.material.color.b, _onColor.a);
            _canHitPlayer = true;
        }

        public void TurnOff()
        {
            _mr.material.color = new Color(_mr.material.color.r, _mr.material.color.g, _mr.material.color.b, _offColor.a);;
            _canHitPlayer = false;
        }

        public void TurnOffExplode()
        {
            //_mr.material.color = new Color(0, 0, 0, 0);
            _mr.material.color = new Color(_mr.material.color.r, _mr.material.color.g, _mr.material.color.b, _offColor.a);;
            _explosion.Play();
            _invokeEvents.AsteroidExploded.Invoke();
            _canHitPlayer = false;
        }

        public void TurnOnFade()
        {
            _mr.material.color = new Color(_mr.material.color.r, _mr.material.color.g, _mr.material.color.b, 0f);
            StartCoroutine(ColorLerp(_onColor, _fadeInTime));
            _canHitPlayer = true;
        }

        IEnumerator ColorLerp(Color col, float fadeTime)
        {
            Color startColor = _mr.material.GetColor("_Color");
            Color endCol = new Color(startColor.r, startColor.g, startColor.b, col.a);
            float startTime = Time.time - Time.deltaTime;
            float interpolationValue = 0f;
            while (interpolationValue <= 1f)
            {
                interpolationValue = (Time.time - startTime) / fadeTime;
                _mr.material.SetColor("_Color", Color.Lerp(startColor, endCol, interpolationValue));
                yield return null;
            }
        }
    }
}
