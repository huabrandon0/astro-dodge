using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    /// <summary>
    /// Stores the current game score. 
    /// The score is added to whenever a block is placed onto the stack.
    /// </summary>
    public class Score : MonoBehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt AddToScore;
        }

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ScoreChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] InvokeEvents _invokeEvents;
        //[SerializeField] DifficultyConfig _diffConfig;

        int _score = 0;

		void OnEnable()
		{
            _responseEvents.AddToScore.AddListener(AddToScore);
		}

		void OnDisable()
		{
            _responseEvents.AddToScore.RemoveListener(AddToScore);
		}

		void AddToScore(int val)
        {
            _score += val;
            _invokeEvents.ScoreChanged.Invoke(_score);
        }
    }
}
