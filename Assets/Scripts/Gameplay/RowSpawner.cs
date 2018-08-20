using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class RowSpawner : MonoBehaviour
    {
        public PooledMonobehaviour[] _prefabs;

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
        }
        
        [SerializeField] InvokeEvents _invokeEvents;

        [SerializeField] DifficultyConfig _diffConfig;
        private float _velocityScale = 1.0f;
        private float _spawnRateScale = 1.0f;

        private Coroutine _spawnCoroutine;

        private void Awake()
        {
            if (_prefabs.Length <= 0)
                Debug.LogError("Prefabs are not initialized.");

            _responseEvents.CountChanged.AddListener(ScaleUp);
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
            count = Mathf.Min(count, capacity);

            // Send out a random row of prefabs.
            List<int> indices = Utility.GenerateRandom(count, 0, capacity);

            foreach (int i in indices)
            {
                PooledMonobehaviour spawned = _prefabs[Random.Range(0, _prefabs.Length)].Get<PooledMonobehaviour>(this.transform, startPosition + new Vector3(i * xSpacing, 0f, Random.Range(0f, zUncertainty)), rotation);
                Rigidbody rb = spawned.GetComponent<Rigidbody>();
                if (rb)
                    rb.velocity = velocity;
            }

            if (_rowUnit != null)
            {
                PooledMonobehaviour rowUnit = _rowUnit.Get<PooledMonobehaviour>(this.transform, startPosition, rotation);
                Rigidbody rowUnitRb = rowUnit.GetComponent<Rigidbody>();
                if (rowUnitRb)
                    rowUnitRb.velocity = velocity;
            }
        }

        public void ScaleUp(int count)
        {
            float oldVelocityScale = _velocityScale;
            float desiredVelocityScale = 1f + _diffConfig.VelocityScaleStep * Mathf.Round(count / _diffConfig.VelocityScaleInterval);
            _velocityScale = Mathf.Min(desiredVelocityScale, _diffConfig.VelocityScaleMax);

            if (_velocityScale != oldVelocityScale)
                _invokeEvents.PlayerSpeedUp.Invoke();

            float desiredSpawnRateScale = 1f + _diffConfig.SpawnRateScaleStep * Mathf.Round(count / _diffConfig.SpawnRateScaleInterval);
            _spawnRateScale = Mathf.Min(desiredSpawnRateScale, _diffConfig.SpawnRateScaleMax);
            
            _rowFillSizeSubtract = Mathf.Min(_diffConfig.RowFillSizeScaleStep * (count / _diffConfig.RowFillSizeScaleInterval), _rowFillSize);
        }

        public void DisableChildren()
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
    }
}
