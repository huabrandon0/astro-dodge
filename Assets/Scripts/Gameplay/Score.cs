using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Extensions;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    /// <summary>
    /// Stores the current game score.
    /// </summary>
    public class Score : MonoBehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt AddToScore;
            public GameEventInt SetScore;
        }

        [SerializeField] ResponseEvents _responseEvents;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ScoreChanged;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        int _score = 0;

		void OnEnable()
		{
            _responseEvents.AddToScore.AddListener(AddToScore);
            _responseEvents.SetScore.AddListener(SetScore);
		}

		void OnDisable()
		{
            _responseEvents.AddToScore.RemoveListener(AddToScore);
            _responseEvents.SetScore.RemoveListener(SetScore);
		}

		public void AddToScore(int val)
        {
            _score += val;
            _invokeEvents.ScoreChanged.Invoke(_score);
        }

        public void SetScore(int val)
        {
            _score = val;
            _invokeEvents.ScoreChanged.Invoke(_score);
        }
    }
}
