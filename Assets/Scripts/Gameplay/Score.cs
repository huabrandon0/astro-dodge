using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Extensions;
using AsteroidRage.Events;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;
using AsteroidRage.Data;

namespace AsteroidRage.Game
{
    /// <summary>
    /// Stores the current game score.
    /// </summary>
    public class Score : Singleton<Score>
    {
        protected Score() { }

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

        void Start()
        {
            Load(GameDataManager.Instance.GetGameData());
            GameDataManager.OnGameDataUpdated += Load;
        }

        public void Load(GameData gameData)
        {
            _highScore = gameData.HighScore;
            _invokeEvents.HighScoreChanged.Invoke(_highScore);
        }

        //public void LoadHighScoreFromGooglePlay()
        //{
        //    LoadLocalHighScore();

        //    if (PlayGamesPlatform.Instance.localUser.authenticated)
        //    {
        //        _debugText.SetText("attempted to get high score from google play");

        //        PlayGamesPlatform.Instance.LoadScores(
        //            GPGSIds.leaderboard_leaderboard,
        //            LeaderboardStart.PlayerCentered,
        //            100, // can this be a lower number???
        //            LeaderboardCollection.Public,
        //            LeaderboardTimeSpan.AllTime,
        //            (data) =>
        //            {
        //                foreach (IScore score in data.Scores)
        //                {
        //                    if (score.userID == PlayGamesPlatform.Instance.GetUserId())
        //                    {
        //                        _debugText.SetText("google play score loaded");
        //                        _highScore = (int)score.value;
        //                        _invokeEvents.HighScoreChanged.Invoke(_highScore);
        //                        SaveLocalHighScore();
        //                        break;
        //                    }
        //                }
        //            });
        //    }
        //}

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
        }

        public void SetScore(int val)
        {
            _score = val;
            _invokeEvents.ScoreChanged.Invoke(_score);
            _invokeEvents.ScoreChangedNoArg.Invoke();
        }

        public void CheckCurrentScoreVsHighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
                _invokeEvents.HighScoreChanged.Invoke(_highScore);
                GameDataManager.Instance.UpdateHighScore(_highScore);
            }
        }
    }
}
