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
            public GameEvent ScoreChangedNoArg;
            public GameEventInt HighScoreChanged;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        int _score = 0;
        int _highScore = 0;

        void Awake()
        {
            LoadScore();
        }

        void Start()
        {
            _invokeEvents.HighScoreChanged.Invoke(_highScore);
        }

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
            if (val != 0)
            {
                _score += val;
                _invokeEvents.ScoreChanged.Invoke(_score);
                _invokeEvents.ScoreChangedNoArg.Invoke();
            }

            CheckCurrentScoreVsHighScore();
        }

        public void SetScore(int val)
        {
            _score = val;
            _invokeEvents.ScoreChanged.Invoke(_score);
            _invokeEvents.ScoreChangedNoArg.Invoke();
            CheckCurrentScoreVsHighScore();
        }

        void SaveScore()
        {
            PlayerPrefs.SetInt("highScore", _highScore);
        }

        void LoadScore()
        {
            _highScore = PlayerPrefs.GetInt("highScore");
        }

        public void CheckCurrentScoreVsHighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
                SaveScore();
                _invokeEvents.HighScoreChanged.Invoke(_highScore);
            }
        }
    }
}
