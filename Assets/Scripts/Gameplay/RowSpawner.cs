using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class RowSpawner : MonoBehaviour
    {
        public PooledMonobehaviour _regularPrefab;
        public PooledMonobehaviour _goldenPrefab;
        float _goldenPercentage = 0f;
        public PooledMonobehaviour _diamondPrefab;
        float _diamondPercentage = 0f;
        int _diamondStartCount = int.MaxValue;

        public PooledMonobehaviour _rowUnit;

        public Vector3 _rowVelocityDirection = Vector3.forward;

        public int _rowSize = 7;
        public int _rowFillSize = 4;
        private int _rowFillSizeSubtract = 0;
        public int _rowFillSizeUncertainty = 1;
        public float _spaceBetweenPrefabs = 1f;
        public float _zPositionUncertainty = 1f;

        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt CountChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent PlayerSpeedUp;
            public GameEvent PlayerSlowDown;
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        [SerializeField] DifficultyConfig _diffConfig;
        private float _velocityScale = 1.0f;
        private float _spawnRateScale = 1.0f;

        private Coroutine _spawnCoroutine;

        private void Awake()
        {
            if (!_regularPrefab)
                Debug.LogError("Regular prefab are not initialized.");

            if (!_goldenPrefab)
                Debug.LogError("Special prefab are not initialized.");

            _responseEvents.CountChanged.AddListener(ScaleUp);

            _goldenPercentage = _diffConfig.StartGoldenSpawnPercent;

            // Prewarm
            _regularPrefab.Get<PooledMonobehaviour>(false);
            _goldenPrefab.Get<PooledMonobehaviour>(false);
            _diamondPrefab.Get<PooledMonobehaviour>(false);
        }

        public void StartSpawningRows()
        {
            _spawnCoroutine = StartCoroutine(SpawnRowsContinuously());
        }

        public void StopSpawningRows()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        private IEnumerator SpawnRowsContinuously()
        {
            while (true)
            {
                SpawnRow();
                yield return new WaitForSeconds(1f / (_diffConfig.StartSpawnRate * _spawnRateScale));
            }
        }

        private void SpawnRow()
        {
            Vector3 startPosition = new Vector3(-1f * (_rowSize - 1) * _spaceBetweenPrefabs / 2, 0f, 0f);
            SpawnRow(_rowFillSize - _rowFillSizeSubtract + Random.Range(0, _rowFillSizeUncertainty + 1), _rowSize, startPosition, Quaternion.identity, _spaceBetweenPrefabs, _zPositionUncertainty, _rowVelocityDirection.normalized * _diffConfig.StartSpeed * _velocityScale);
        }

        private void SpawnRow(int count, int capacity, Vector3 startPosition, Quaternion rotation, float xSpacing, float zUncertainty, Vector3 velocity)
        {
            bool[] contains = new bool[3] { false, false, false };
            int j = 0;

            count = Mathf.Min(count, capacity);

            // Send out a random row of prefabs.
            List<int> indices = Utility.GenerateRandom(count, 0, capacity);

            foreach (int i in indices)
            {
                PooledMonobehaviour currentLevelPrefab = _regularPrefab;
                PooledMonobehaviour nextLevelPrefab = _goldenPrefab;
                float nextLevelPercentage = _goldenPercentage;

                if (nextLevelPercentage >= 1f)
                {
                    j = 1;
                    currentLevelPrefab = _goldenPrefab;
                    nextLevelPrefab = _diamondPrefab;
                    nextLevelPercentage = _diamondPercentage;
                }

                if (Random.Range(0f, 1f) < nextLevelPercentage)
                {
                    contains[j + 1] = true;
                    PooledMonobehaviour spawned = nextLevelPrefab.Get<PooledMonobehaviour>(this.transform, startPosition + new Vector3(i * xSpacing, 0f, Random.Range(0f, zUncertainty)), rotation);
                    Rigidbody rb = spawned.GetComponent<Rigidbody>();
                    if (rb)
                        rb.velocity = velocity;
                }
                else
                {
                    contains[j] = true;
                    PooledMonobehaviour spawned = currentLevelPrefab.Get<PooledMonobehaviour>(this.transform, startPosition + new Vector3(i * xSpacing, 0f, Random.Range(0f, zUncertainty)), rotation);
                    Rigidbody rb = spawned.GetComponent<Rigidbody>();
                    if (rb)
                        rb.velocity = velocity;
                }
            }

            if (_rowUnit != null)
            {
                PooledMonobehaviour rowUnit = _rowUnit.Get<PooledMonobehaviour>(this.transform, startPosition, rotation);
                Rigidbody rowUnitRb = rowUnit.GetComponent<Rigidbody>();
                if (rowUnitRb)
                    rowUnitRb.velocity = velocity;
                RowUnit ru = rowUnit.GetComponent<RowUnit>();
                if (ru)
                {
                    ru.containsAsteroids = contains[0];
                    ru.containsGoldenAsteroids = contains[1];
                    ru.containsDiamondAsteroids = contains[2];
                }
            }
        }

        public void ScaleUp(int count)
        {
            if (count == 0)
            {
                _goldenPercentage = _diffConfig.StartGoldenSpawnPercent;
                _diamondPercentage = _diffConfig.StartDiamondSpawnPercent;
                _diamondStartCount = 0;
            }

            float oldVelocityScale = _velocityScale;
            float desiredVelocityScale = 1f + _diffConfig.VelocityScaleStep * Mathf.Round(count / _diffConfig.VelocityScaleInterval);
            _velocityScale = Mathf.Min(desiredVelocityScale, _diffConfig.VelocityScaleMax);

            if (_velocityScale > oldVelocityScale)
                _invokeEvents.PlayerSpeedUp.Invoke();
            else if (_velocityScale < oldVelocityScale)
                _invokeEvents.PlayerSlowDown.Invoke();

            float desiredSpawnRateScale = 1f + _diffConfig.SpawnRateScaleStep * Mathf.Round(count / _diffConfig.SpawnRateScaleInterval);
            _spawnRateScale = Mathf.Min(desiredSpawnRateScale, _diffConfig.SpawnRateScaleMax);
            
            _rowFillSizeSubtract = Mathf.Min(_diffConfig.RowFillSizeScaleStep * (count / _diffConfig.RowFillSizeScaleInterval), _rowFillSize);

            if (_goldenPercentage < 1f)
            {
                _goldenPercentage = _diffConfig.StartGoldenSpawnPercent + Mathf.Round(count / _diffConfig.GoldenSpawnPercentAddInterval) * _diffConfig.GoldenSpawnPercentAddStep;
                if (_goldenPercentage > _diffConfig.GoldenSpawnPercentMaxBeforeSnap)
                {
                    _goldenPercentage = 1f;
                    _diamondStartCount = count;
                }
            }
            else if (_diamondPercentage != 1f)
            {
                _diamondPercentage = _diffConfig.StartDiamondSpawnPercent + Mathf.Round((count - _diamondStartCount)/ _diffConfig.DiamondSpawnPercentAddInterval) * _diffConfig.DiamondSpawnPercentAddStep;
                if (_diamondPercentage > _diffConfig.DiamondSpawnPercentMaxBeforeSnap)
                    _diamondPercentage = 1f;
            }
        }

        public void DisableChildren()
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
    }
}
