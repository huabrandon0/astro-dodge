using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    [RequireComponent(typeof(Light))]
    public class SetLightColor : MonoBehaviour
    {
        Light _light;

        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventVerticalGradient SetLightColor;
            public GameEventVerticalGradient TransitionToLightColor;
        }

        [SerializeField] ResponseEvents _responseEvents;

        void Awake()
        {
            _light = GetComponent<Light>();
        }

        void OnEnable()
        {
            _responseEvents.SetLightColor.AddListener(SetColor);
            _responseEvents.TransitionToLightColor.AddListener(SetColor);
        }

        void OnDisable()
        {
            _responseEvents.SetLightColor.RemoveListener(SetColor);
            _responseEvents.TransitionToLightColor.RemoveListener(SetColor);
        }

        void SetColor(VerticalGradient verticalGradient)
        {
            _light.color = verticalGradient._bottomColor;
        }
    }
}