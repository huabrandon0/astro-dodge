using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSpawner : Singleton<RowSpawner>
{
    protected RowSpawner() { }

    public PooledMonobehaviour prefab;

    public float timeBetweenRows = 1f;

    public Vector3 rowVelocity = Vector3.forward;

    public int rowSize = 7;
    public int prefabsPerRow = 5;
    public float spaceBetweenPrefabs = 1f;

    private void Awake()
    {
        if (!this.prefab)
            Debug.LogError("GameObject objectToSpawn is not initialized.");
    }

    public IEnumerator SpawnRowsContinuously()
    {
        while (true)
        {
            SpawnRow();
            yield return new WaitForSeconds(this.timeBetweenRows);
        }
    }

    private void SpawnRow()
    {
        Vector3 startPosition = new Vector3(-1f * (this.rowSize - 1) * this.spaceBetweenPrefabs / 2, 0f, 0f);
        SpawnRow(this.prefabsPerRow, this.rowSize, startPosition, Quaternion.identity, this.spaceBetweenPrefabs, this.rowVelocity);
    }

    private void SpawnRow(int count, int capacity, Vector3 startPosition, Quaternion rotation, float xSpacing, Vector3 velocity)
    {
        // Send out a random row of prefabs.
        List<int> indices = Utility.GenerateRandom(count, 0, capacity);

        foreach (int i in indices)
        {
            PooledMonobehaviour spawned = this.prefab.Get<PooledMonobehaviour>(this.transform, startPosition + new Vector3(i * xSpacing, 0f, 0f), rotation);
            Rigidbody rb = spawned.GetComponent<Rigidbody>();
            if (rb)
                rb.velocity = velocity;
        }
    }
}
