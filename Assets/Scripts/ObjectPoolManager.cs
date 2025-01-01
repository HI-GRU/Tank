using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance => instance;

    public static readonly string missileTag = "missile";
    public static readonly List<string> pyramidTags = new List<string> { "pyramid_0", "pyramid_1", "pyramid_2", "pyramid_3" };
    public static readonly string obstacleSign = "obstacleSign";
    public static readonly string weaponSign = "weaponSign";


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (instance == null) instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    private void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                PooledObject pooledObj = obj.GetComponent<PooledObject>();
                if (pooledObj != null) pooledObj.poolTag = pool.tag;
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        Queue<GameObject> pool = poolDictionary[tag];

        if (pool.Count == 0)
        {
            Pool poolInfo = pools.Find(p => p.tag == tag);
            GameObject newObj = Instantiate(poolInfo.prefab);
            return SetupPooledObject(newObj, position, rotation);
        }

        GameObject spawnedObj = pool.Dequeue();
        return SetupPooledObject(spawnedObj, position, rotation);
    }

    private GameObject SetupPooledObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        IPooledObject pooledObj = obj.GetComponent<IPooledObject>();
        if (pooledObj != null) pooledObj.InitializeObject();

        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return;
        }

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
