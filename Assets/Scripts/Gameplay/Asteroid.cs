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

        [SerializeField] float fadeInTime;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _mr = GetComponentInChildren<MeshRenderer>();
            _col = GetComponentInChildren<Collider>();

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
            TurnOnFade();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _invokeEvents.PlayerHit.Invoke();
            else if (other.CompareTag("HitZone"))
                TurnOff();
            else if (other.CompareTag("Shield"))
            {
                // TODO: Explode asteroid effect
                _col.enabled = false;
            }
        }

        public void TurnOn()
        {
            _mr.material.color = _onColor;
            _col.enabled = true;
        }

        public void TurnOff()
        {
            _mr.material.color = _offColor;
            _col.enabled = false;
        }

        public void TurnOnFade()
        {
            _mr.material.color = new Color(0, 0, 0, 0);
            StartCoroutine(ColorLerp(_onColor, fadeInTime));
            _col.enabled = true;
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
