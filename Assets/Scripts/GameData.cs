﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Data
{
    [System.Serializable]
    public class GameData
    {
        public GameData(int highScore = 0, int currency = 0, bool[] unlockedShips = null)
        {
            HighScore = highScore;
            Currency = currency;

            if (unlockedShips != null)
                UnlockedShips = unlockedShips.Clone() as bool[];
            else
                UnlockedShips = null;
        }

        public GameData(GameData gameData)
        {
            HighScore = gameData.HighScore;
            Currency = gameData.Currency;

            if (gameData.UnlockedShips != null)
                UnlockedShips = gameData.UnlockedShips.Clone() as bool[];
            else
                gameData.UnlockedShips = null;
        }

        public int HighScore;
        public int Currency;
        public bool[] UnlockedShips;

        public override string ToString()
        {
            string ret = "HighScore: " + HighScore + " \n" +
                "Currency: " + Currency + " \n" +
                "UnlockedShips: ";

            if (UnlockedShips != null && UnlockedShips.Length > 0)
            {
                for (int i = 0; i < UnlockedShips.Length; i++)
                {
                    ret += UnlockedShips[i].ToString() + ", ";
                }
            }
            else
            {
                ret += "null or empty";
            }

            ret += "\n JSON: " + GameDataToString(this);

            return ret;
        }

        public static string GameDataToString(GameData gameData)
        {
            return JsonUtility.ToJson(gameData);
        }

        public static GameData StringToGameData(string gameDataString)
        {
            return JsonUtility.FromJson<GameData>(gameDataString);
        }

        public static string SaveKey = "mySave";
    }
}