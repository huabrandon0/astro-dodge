using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        protected ScoreManager() { }

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent ScoreChanged;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        public int Score { get; private set; }

        public void ResetScore()
        {
            Score = 0;
            _invokeEvents.ScoreChanged.Invoke();
        }

		public void IncrementScore()
        {
            Score++;
            Debug.Log(Score);
            _invokeEvents.ScoreChanged.Invoke();
        }
    }
}
