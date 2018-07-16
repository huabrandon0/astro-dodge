using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage
{
    [CreateAssetMenu(menuName = "Data/DifficultyConfig")]
    public class DifficultyConfig : ScriptableObject
    {
        public int VelocityScaleInterval = 5;
        public float VelocityScaleStep = 0.1f;

        public int SpawnRateScaleInterval = 5;
        public float SpawnRateScaleStep = 0.1f;

        public int MoveSpeedScaleInterval = 5;
        public float MoveSpeedScaleStep = 0.1f;

        public float DodgeDistance = 1.5f;
        public int DodgeScore = 3;
    }
}
