using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class RandomRotator : MonoBehaviour
    {
        [SerializeField] DifficultyConfig _diffConfig;

        float _tumble = 1f;

        void OnEnable()
        {
            ScaleUp(Counter._count);
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * _tumble;
        }

        public void ScaleUp(int count)
        {
            _tumble = 1f + _diffConfig.TumbleScaleStep * Mathf.Round(count / _diffConfig.TumbleScaleInterval);
        }
    }
}
