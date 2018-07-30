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

        public int SpawnRateScaleInterval = 5;
        public float SpawnRateScaleStep = 0.1f;

        public int MoveSpeedScaleInterval = 5;
        public float MoveSpeedScaleStep = 0.05f;

        public float DodgeDistance = 1.5f;
        public int DodgeScore = 5;

        public int TumbleScaleInterval = 5;
        public float TumbleScaleStep = 0.1f;

        public int CountInterval = 1;
        public int ScorePerCount = 10;
    }
}
