using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class ScoreManager : MonoBehaviour
    {
        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ScoreChanged;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        public int Score { get; private set; }

        public void ResetScore()
        {
            Score = 0;
            _invokeEvents.ScoreChanged.Invoke(Score);
        }

		public void IncrementScore()
        {
            Score++;
            Debug.Log(Score);
            _invokeEvents.ScoreChanged.Invoke(Score);
        }
    }
}
