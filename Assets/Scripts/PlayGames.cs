using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using AsteroidRage.Game;

namespace AsteroidRage.GPG
{
    public class PlayGames : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _debugText;

        void Awake()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
            PlayGamesPlatform.DebugLogEnabled = true; // remove l8r
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();

            Score.Instance.LoadLocalHighScore();
            SignIn();
        }

        public void SignIn()
        {
            _debugText.SetText("sign in!");
            if (!PlayGamesPlatform.Instance.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
            }
        }

        void SignInCallback(bool success)
        {
            if (success)
            {
                Score.Instance.LoadHighScoreFromGooglePlay();
                _debugText.SetText("successful sign in as " + PlayGamesPlatform.Instance.GetUserDisplayName());
            }
            else
                _debugText.SetText("failed sign in as" + PlayGamesPlatform.Instance.GetUserDisplayName());
        }

        public void ShowLeaderboardsUI()
        {
            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                _debugText.SetText("showing leaderboards");
                PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
            }
            else
            {
                SignIn();
            }
        }
    }
}
