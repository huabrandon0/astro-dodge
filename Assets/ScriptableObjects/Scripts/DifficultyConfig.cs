using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage
{
    [CreateAssetMenu(menuName = "Data/DifficultyConfig")]
    public class DifficultyConfig : ScriptableObject
    {
        public float StartSpeed = 6f;
        public int VelocityScaleInterval = 5;
        public float VelocityScaleStep = 0.3f;
        public float VelocityScaleMax = 7.6f;

        public float StartSpawnRate = 0.5f;
        public int SpawnRateScaleInterval = 5;
        public float SpawnRateScaleStep = 0.25f;
        public float SpawnRateScaleMax = 6.50f;

        public float StartGoldenSpawnPercent = 0.01f;
        public int GoldenSpawnPercentAddInterval = 10;
        public float GoldenSpawnPercentAddStep = 0.08f;
        public float GoldenSpawnPercentMaxBeforeSnap = 0.7f;

        public float StartDiamondSpawnPercent = 0.09f;
        public int DiamondSpawnPercentAddInterval = 5;
        public float DiamondSpawnPercentAddStep = 0.08f;
        public float DiamondSpawnPercentMaxBeforeSnap = 0.7f;

        public int RowFillSizeScaleInterval = 100;
        public int RowFillSizeScaleStep = 0;

        public float StartMoveSpeed = 4.4f;
        public int MoveSpeedScaleInterval = 5;
        public float MoveSpeedScaleStep = 0.12f;
        public float MoveSpeedScaleMax = 3.16f;

        public float DodgeDistance = 1.5f;
        public int DodgeScore = 0;

        public int TumbleScaleInterval = 5;
        public float TumbleScaleStep = 0.12f;

        public int CountInterval = 1;
        public int ScorePerCount = 0;

        public int BoostCount = 15;
    }
}
