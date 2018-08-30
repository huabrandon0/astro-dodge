using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace AsteroidRage.Data
{
    public class PlayGames : Singleton<PlayGames>
    {
        protected PlayGames() { }

        void Awake()
        {
            Debug.Log("PlayGames.Awake");
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            SignIn();
        }

        public void SignIn()
        {
            Debug.Log("SignIn");
            if (!PlayGamesPlatform.Instance.IsAuthenticated())
                PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }

        void SignInCallback(bool success)
        {
            if (success)
            {
                Debug.Log("Successful sign in as " + PlayGamesPlatform.Instance.GetUserDisplayName());
                GameDataManager.Instance.LoadGame();
            }
            else
                Debug.Log("Failed sign in");
        }

        public void ShowLeaderboardsUI()
        {
            Debug.Log("ShowLeaderboardsUI");
            if (PlayGamesPlatform.Instance.IsAuthenticated())
                PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
            else
                SignIn();
        }
    }
}
