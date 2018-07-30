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

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ScoreChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;
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

		void AddToScore(int val)
        {
            _score += val;
            _invokeEvents.ScoreChanged.Invoke(_score);
        }

        void SetScore(int val)
        {
            _score = val;
            _invokeEvents.ScoreChanged.Invoke(_score);
        }
    }
}
