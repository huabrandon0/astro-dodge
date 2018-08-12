using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class ExhaustEffects : MonoBehaviour
    {
        [SerializeField] ParticleSystem[] _flares;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent PlayerFlaredEvent;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

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
    }
}
