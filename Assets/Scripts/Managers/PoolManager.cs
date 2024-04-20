using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Pool[] pools;

    private static Dictionary<string, Pool> cache;
    private static PoolManager poolManager;

    public static PoolManager instance
    {
        get
        {
            if (!poolManager)
            {
                poolManager = FindObjectOfType(typeof(PoolManager)) as PoolManager;

                if (!poolManager)
                {
                    Debug.LogError("There needs to be one active PoolManger script on a GameObject in your scene.");
                }
                else
                {
                    poolManager.Init();
                }
            }

            return poolManager;
        }
    }

    void Init()
    {
        if (cache == null)
        {
            cache = new Dictionary<string, Pool>();
        }
    }

    void Start ()
    {
        if (pools != null)
        {
            cache = new Dictionary<string, Pool>(pools.Length);

            for (int i = 0; i < pools.Length; i++)
            {
                Pool tempPool = pools[i];
                cache[tempPool.key] = new Pool(tempPool.key, tempPool.poolObject, tempPool.size, tempPool.parentingGroup, tempPool.expandable);
            }
        }
	}

    /// <summary>
    /// Grabs the next item from the pool.
    /// </summary>
    /// <param name="key">Name of the pool to draw from.</param>
    /// <returns>Next free item.  Null if none available.</returns>
    public static GameObject Pull(string key)
    {
        return (cache[key].Pull());
    }

    public static GameObject Pull(string key, Vector3 position, Quaternion rotation)
    {
        GameObject clone = cache[key].Pull();
        clone.transform.position = position;
        clone.transform.rotation = rotation;
        return clone;
    }
}

[System.Serializable]
public class Pool
{
    public string key;
    public GameObject poolObject;
    public int size;
    public Transform parentingGroup;
    public bool expandable;

    private List<GameObject> pool;

    public Pool(string keyName, GameObject obj, int count, Transform parent = null, bool dynamicExpansion = false)
    {
        key = keyName;
        poolObject = obj;
        size = count;
        expandable = dynamicExpansion;
        parentingGroup = parent;
        pool = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            AddItem();
        }
    }

    public GameObject Pull()
    {
        // Is there one ready?
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        // Can one be added?
        if (expandable)
        {
            return AddItem(true);
        }
        else
        {
            Debug.LogWarning("No available item from pool with key: " + key);
            return null;
        }
    }

    private GameObject AddItem(bool keepActive = false)
    {
        int index = pool.Count;
        pool.Add(UnityEngine.Object.Instantiate(poolObject));
        pool[index].name = poolObject.name + "_" + index.ToString().PadLeft(4, '0');
        pool[index].SetActive(keepActive);
        if (parentingGroup != null)
        {
            pool[index].transform.parent = parentingGroup;
        }
        return pool[index];
    }
}