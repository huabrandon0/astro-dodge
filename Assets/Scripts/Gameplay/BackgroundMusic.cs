using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using AsteroidRage.Extensions;

namespace AsteroidRage.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        AudioSource _audioSource;

        float _originalPitch;

        [SerializeField] float _slowPitch = 0f;

        void Awake()
        {
            _audioSource = this.GetComponentAssert<AudioSource>();
            _originalPitch = _audioSource.pitch;
        }

        public void SlowDown(float time)
        {
            StartCoroutine(SlowPitch(time, _slowPitch));
        }

        IEnumerator SlowPitch(float time, float endPitch)
        {
            float startPitch = _audioSource.pitch;
            float startTime = Time.time - Time.deltaTime;
            float interpolationValue = 0f;
            while (interpolationValue <= 1f)
            {
                interpolationValue = (Time.time - startTime) * (1 / time);
                _audioSource.pitch = Mathf.Lerp(startPitch, endPitch, interpolationValue);
                yield return null;
            }

            _audioSource.pitch = _originalPitch;
        }
    }
}
