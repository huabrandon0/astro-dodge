using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GooglePlayGames;

namespace AsteroidRage.Data
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        protected GameDataManager() { }

        GameData _gameData;

        const string _SAVE_KEY = "saveKey";

        void Awake()
        {
            LoadGame();
            SaveGame();
        }

        public GameData GetGameData()
        {
            return new GameData(_gameData);
        }

        public void UpdateHighScore(int val)
        {
            _gameData.HighScore = val;
        }

        public void UpdateCurrency(int val)
        {
            _gameData.Currency = val;
        }

        public void UpdateUnlockedShips(bool[] unlockedShips)
        {
            _gameData.UnlockedShips = unlockedShips.Clone() as bool[];
        }

        public void UpdateLastChosenShip(int val)
        {
            _gameData.LastChosenShip = val;
        }

        public void LoadGame()
        {
            _gameData = Resolve(GetLocalGameData(), GetCloudGameData());
            Debug.Log("load: \n" + _gameData);
        }

        public void SaveGame()
        {
            Debug.Log("save: \n" + _gameData);
            SaveLocalGameData();
            SaveCloudGameData();
            UpdateLeaderboards();
        }

        public GameData GetLocalGameData()
        {
            if (PlayerPrefs.HasKey(_SAVE_KEY))
            {
                string stringData = PlayerPrefs.GetString(_SAVE_KEY);
                GameData gameData = GameData.StringToGameData(stringData);
                return gameData;
            }
            else
            {
                GameData gameData = new GameData();
                return gameData;
            }
        }

        public GameData GetCloudGameData()
        {
            return new GameData();
        }

        public void SaveLocalGameData()
        {
            PlayerPrefs.SetString(_SAVE_KEY, GameData.GameDataToString(_gameData));
        }

        public void SaveCloudGameData()
        {

        }

        public void UpdateLeaderboards()
        {
            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.ReportScore(_gameData.HighScore, GPGSIds.leaderboard_leaderboard, (bool success) => { Debug.Log("Update leaderboard success: " + success); });
            }
        }

        public GameData Resolve(GameData gameData1, GameData gameData2)
        {
            if (gameData1 == null || gameData2 == null)
                return new GameData();

            GameData ret = new GameData();
            ret.HighScore = Mathf.Max(gameData1.HighScore, gameData2.HighScore);
            ret.Currency = Mathf.Max(gameData1.Currency, gameData2.Currency);

            if (gameData1.UnlockedShips == null)
            {
                ret.UnlockedShips = gameData2.UnlockedShips;
            }
            else if (gameData2.UnlockedShips == null)
            {
                ret.UnlockedShips = gameData1.UnlockedShips;
            }
            else
            {
                bool[] lower;
                bool[] upper;
                if (gameData1.UnlockedShips.Length > gameData2.UnlockedShips.Length)
                {
                    lower = gameData2.UnlockedShips;
                    upper = gameData1.UnlockedShips;
                }
                else
                {
                    lower = gameData1.UnlockedShips;
                    upper = gameData2.UnlockedShips;
                }

                ret.UnlockedShips = new bool[upper.Length];
                for (int i = 0; i < lower.Length; i++)
                {
                    ret.UnlockedShips[i] = lower[i] || upper[i];
                }

                for (int i = lower.Length; i < upper.Length; i++)
                {
                    ret.UnlockedShips[i] = upper[i];
                }
            }

            ret.LastChosenShip = gameData1.LastChosenShip;

            return ret;
        }
    }
}
