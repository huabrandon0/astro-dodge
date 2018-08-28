using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    public class TimeTravel : MonoBehaviour
    {
        public float _fastTimeScale = 5f;
        public float _slowTimeScale = 1f;

        public bool _fastMode = false;

        void Update()
        {
            if (_fastMode)
                Time.timeScale = _fastTimeScale;
            else
                Time.timeScale = _slowTimeScale;
        }
    }
}
