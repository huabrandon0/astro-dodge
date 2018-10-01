using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using System;

namespace AsteroidRage.Data
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        protected GameDataManager() { }

        public static event Action<GameData> OnGameDataUpdated = delegate { };

        GameData _gameData;

        GameData _cloudGameData;

        const string _SAVE_KEY = "saveKey";

        void Awake()
        {
            Debug.Log("GameDataManager.Awake");
            LoadGameLocal();
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

        public void LoadGame()
        {
            Debug.Log("LoadGame");

            LoadGameLocal();

            // Attempt to load cloud game data.
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                savedGameClient.OpenWithAutomaticConflictResolution(_SAVE_KEY, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseOriginal, OnSavedGameOpenedLoading);
            }
        }

        void LoadGameLocal()
        {
            if (PlayerPrefs.HasKey(_SAVE_KEY))
            {
                string stringData = PlayerPrefs.GetString(_SAVE_KEY);
                _gameData = GameData.StringToGameData(stringData);
            }
            else
                _gameData = new GameData();
        }

        void OnSavedGameOpenedLoading(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            Debug.Log("OnSavedGameOpenedLoading - SavedGameRequestStatus (saved game metadata retrieved): " + status);
            if (status == SavedGameRequestStatus.Success)
                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
        }

        void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
        {
            Debug.Log("OnSavedGameDataRead - SavedGameRequestStatus (bytes read from metadata): " + status);
            if (status == SavedGameRequestStatus.Success && savedData.Length > 0)
            {
                string cloudDataString = Encoding.ASCII.GetString(savedData);
                UpdateGameData(GameData.StringToGameData(cloudDataString));
            }
        }

        public void SaveGame()
        {
            Debug.Log("SaveGame - Saving the following data: \n" + _gameData);

            // Save game locally.
            PlayerPrefs.SetString(_SAVE_KEY, GameData.GameDataToString(_gameData));

            // Attempt to save game on the cloud.
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                savedGameClient.OpenWithAutomaticConflictResolution(_SAVE_KEY, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseOriginal, OnSavedGameOpenedSaving);
            }
            
            UpdateLeaderboards();
        }

        public void ResetGame()
        {
            _gameData = new GameData();
            SaveGame();
        }

        void OnSavedGameOpenedSaving(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            Debug.Log("OnSavedGameOpenedSaving - SavedGameRequestStatus (opened a saved game to written to): " + status);
            if (status == SavedGameRequestStatus.Success)
            {
                string stringData = GameData.GameDataToString(_gameData);
                byte[] byteData = Encoding.ASCII.GetBytes(stringData);
                
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
                PlayGamesPlatform.Instance.SavedGame.CommitUpdate(game, update, byteData, OnSavedGameDataWritten);
            }
        }
        
        void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            Debug.Log("OnSavedGameDataWritten - SavedGameRequestStatus (game data written to cloud): " + status);
        }

        public void UpdateLeaderboards()
        {
            if (PlayGamesPlatform.Instance.localUser.authenticated)
                PlayGamesPlatform.Instance.ReportScore(_gameData.HighScore, GPGSIds.leaderboard_leaderboard, (bool success) => { Debug.Log("Update leaderboard success: " + success); });
        }

        void UpdateGameData(GameData newGameData)
        {
            Debug.Log("Updating game data with new data");
            _gameData = Resolve(_gameData, newGameData);
            SaveGame();
            OnGameDataUpdated.Invoke(_gameData);
        }

        GameData Resolve(GameData gameData1, GameData gameData2)
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

            return ret;
        }
    }
}
