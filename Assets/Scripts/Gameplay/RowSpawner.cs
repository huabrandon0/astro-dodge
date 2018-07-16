using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class RowSpawner : MonoBehaviour
    {
        public PooledMonobehaviour _prefab;

        public float _spawnRate = 0.5f;

        public Vector3 _rowVelocity = Vector3.forward;

        public int _rowSize = 7;
        public int _rowFillSize = 4;
        public int _rowFillSizeUncertainty = 1;
        public float _spaceBetweenPrefabs = 1f;
        public float _zPositionUncertainty = 1f;

        [SerializeField] GameEventInt _countChanged;

        [SerializeField] DifficultyConfig _diffConfig;
        private float _velocityScale = 1.0f;
        private float _spawnRateScale = 1.0f;

        private void Awake()
        {
            if (!_prefab)
                Debug.LogError("GameObject objectToSpawn is not initialized.");

            _countChanged.AddListener(ScaleUp);
        }

        public void StartSpawningRows()
        {
            StartCoroutine(SpawnRowsContinuously());
        }

        private IEnumerator SpawnRowsContinuously()
        {
            while (true)
            {
                SpawnRow();
                yield return new WaitForSeconds(1f / (_spawnRate * _spawnRateScale));
            }
        }

        private void SpawnRow()
        {
            Vector3 startPosition = new Vector3(-1f * (_rowSize - 1) * this._spaceBetweenPrefabs / 2, 0f, 0f);
            SpawnRow(_rowFillSize + Random.Range(0, _rowFillSizeUncertainty + 1), _rowSize, startPosition, Quaternion.identity, _spaceBetweenPrefabs, _zPositionUncertainty, _rowVelocity * _velocityScale);
        }

        private void SpawnRow(int count, int capacity, Vector3 startPosition, Quaternion rotation, float xSpacing, float zUncertainty, Vector3 velocity)
        {
            count = Mathf.Min(count, capacity);

            // Send out a random row of prefabs.
            List<int> indices = Utility.GenerateRandom(count, 0, capacity);

            foreach (int i in indices)
            {
                PooledMonobehaviour spawned = _prefab.Get<PooledMonobehaviour>(this.transform, startPosition + new Vector3(i * xSpacing, 0f, Random.Range(0f, zUncertainty)), rotation);
                Rigidbody rb = spawned.GetComponent<Rigidbody>();
                if (rb)
                    rb.velocity = velocity;
            }
        }

        public void ScaleUp(int count)
        {
            _velocityScale = 1f + _diffConfig.VelocityScaleStep * Mathf.Round(count / _diffConfig.VelocityScaleInterval);
            _spawnRateScale = 1f + _diffConfig.SpawnRateScaleStep * Mathf.Round(count / _diffConfig.SpawnRateScaleInterval);
        }
    }
}
