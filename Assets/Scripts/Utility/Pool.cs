using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pool : MonoBehaviour
{
    private static Dictionary<PooledMonobehaviour, Pool> pools = new Dictionary<PooledMonobehaviour, Pool>();

    private Queue<PooledMonobehaviour> objects = new Queue<PooledMonobehaviour>();
    private List<PooledMonobehaviour> disabledObjects = new List<PooledMonobehaviour>();

    private PooledMonobehaviour prefab;

    void Awake()
    {
        // If the scene is unloaded, pooled objects no longer exist, so we must clear the dictionary of pools.
        // Though there exists a better alternative: make the pool persist between scenes.
        SceneManager.sceneUnloaded += (Scene current) => { pools = new Dictionary<PooledMonobehaviour, Pool>(); };
    }

    public static Pool GetPool(PooledMonobehaviour prefab)
    {
        if (pools.ContainsKey(prefab))
            return pools[prefab];

        var pool = new GameObject("Pool-" + (prefab as Component).name).AddComponent<Pool>();
        pool.prefab = prefab;

        pool.GrowPool();
        pools.Add(prefab, pool);
        return pool;
    }

    public T Get<T>() where T : PooledMonobehaviour
    {
        if (objects.Count == 0)
        {
            GrowPool();
        }

        var pooledObject = objects.Dequeue();

        return pooledObject as T;
    }

    public void GrowPool()
    {
        for (int i = 0; i < prefab.InitialPoolSize; i++)
        {
            var pooledObject = Instantiate(this.prefab) as PooledMonobehaviour;
            (pooledObject as Component).gameObject.name += " " + i;

            pooledObject.OnDestroyEvent += () => AddObjectToAvailable(pooledObject);

            (pooledObject as Component).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        MakeDisabledObjectsChildren();
    }

    private void MakeDisabledObjectsChildren()
    {
        if (disabledObjects.Count > 0)
        {
            foreach (var pooledObject in disabledObjects)
            {
                if (pooledObject.gameObject.activeInHierarchy == false)
                {
                    (pooledObject as Component).transform.SetParent(transform);
                }
            }

            disabledObjects.Clear();
        }
    }

    private void AddObjectToAvailable(PooledMonobehaviour pooledObject)
    {
        disabledObjects.Add(pooledObject);
        objects.Enqueue(pooledObject);
    }
}
