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

        void Awake()
        {
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
                TurnOffExplode();
            }
            else if (other.CompareTag("Shield"))
            {
                // TODO: Explode asteroid effect
                _canHitPlayer = false;
            }
        }

        public void TurnOn()
        {
            _mr.material.color = _onColor;
            _canHitPlayer = true;
        }

        public void TurnOff()
        {
            _mr.material.color = _offColor;
            _canHitPlayer = false;
        }

        public void TurnOffExplode()
        {
            //_mr.material.color = new Color(0, 0, 0, 0);
            _mr.material.color = _offColor;
            _explosion.Play();
            _canHitPlayer = false;
        }

        public void TurnOnFade()
        {
            _mr.material.color = new Color(0, 0, 0, 0);
            StartCoroutine(ColorLerp(_onColor, _fadeInTime));
            _canHitPlayer = true;
        }

        IEnumerator ColorLerp(Color col, float fadeTime)
        {
            Color startColor = _mr.material.GetColor("_Color");
            float startTime = Time.time - Time.deltaTime;
            float interpolationValue = 0f;
            while (interpolationValue <= 1f)
            {
                interpolationValue = (Time.time - startTime) / fadeTime;
                _mr.material.SetColor("_Color", Color.Lerp(startColor, col, interpolationValue));
                yield return null;
            }
        }
    }
}
