using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject prefab;
    public string tag;
    public int size;
    public bool expandable;
}
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;
    Dictionary<string, Queue<GameObject>> objectPool;

    public static ObjectPool Instance;
    private void Awake()
    {
        Instance = this;
        objectPool = new Dictionary<string, Queue<GameObject>>();
        InitialSpawning();
    }

    private void InitialSpawning()
    {
        foreach (var pool in pools)
        {
            Queue<GameObject> pooledObjects = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                var instance = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity, transform);

                instance.SetActive(false);
                pooledObjects.Enqueue(instance);

            }
            objectPool.Add(pool.tag, pooledObjects);
        }
    }

    public GameObject OnSpawnObject(string tag, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!objectPool.ContainsKey(tag))
        {
            Debug.LogWarning($"Does't contain a key \"{tag}\".");
            return null;
        }
        var spawningObject = objectPool[tag].Dequeue();
        
        if(spawningObject.activeSelf)
        {
            spawningObject = TryToExtend(tag);
            if (spawningObject == null) return null;
        }

        spawningObject.transform.position = position;
        spawningObject.transform.rotation = rotation;
        spawningObject.transform.parent = parent;
        spawningObject.SetActive(true);

        var pooledObjects = spawningObject.GetComponents<IPolledObject>();
        if (pooledObjects != null)
        {
            foreach (var pooledObject in pooledObjects)
            {
                pooledObject.OnSpawn();
            }
        }

        objectPool[tag].Enqueue(spawningObject);

        return spawningObject;
    }

    private GameObject TryToExtend(string tag)
    {
        var pool = pools.Find(each => each.tag == tag);
        if (pool.expandable)
        {
            var instance = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity, transform);
            instance.SetActive(false);
            objectPool[tag].Enqueue(instance);
            return instance;
        }
        Debug.LogWarning("No free objects in the pool.");
        return null;
    }
}
