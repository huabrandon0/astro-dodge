using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage
{
    [CreateAssetMenu(menuName = "Data/DifficultyConfig")]
    public class DifficultyConfig : ScriptableObject
    {
        public float StartSpeed = 5f;
        public int VelocityScaleInterval = 5;
        public float VelocityScaleStep = 0.1f;
        //public float VelocityScaleMax = 2.5f;

        public float StartSpawnRate = 0.4f;
        public int SpawnRateScaleInterval = 5;
        public float SpawnRateScaleStep = 0.1f;
        public float SpawnRateScaleMax = 2f;

        public int RowFillSizeScaleInterval = 50;
        public int RowFillSizeScaleStep = 1;

        public float StartMoveSpeed = 5f;
        public int MoveSpeedScaleInterval = 5;
        public float MoveSpeedScaleStep = 0.05f;
        public float MoveSpeedScaleMax = 1.75f;

        public float DodgeDistance = 1.5f;
        public int DodgeScore = 5;

        public int TumbleScaleInterval = 5;
        public float TumbleScaleStep = 0.1f;

        public int CountInterval = 1;
        public int ScorePerCount = 10;

        public int BoostCount = 15;
    }
}
