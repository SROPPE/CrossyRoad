
using System.Collections.Generic;
using UnityEngine;

public class GrassChunkFiller : MonoBehaviour,IPolledObject
{
    [SerializeField] private List<GameObject> grassPrefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float spawnChance = 0.05f;
    private bool canSpawn = false;
    private ObjectPool objectPool;
    private void Awake()
    {
        objectPool = ObjectPool.Instance;
    }
    private void StartChunkFilling()
    {
        if (!canSpawn) return;
        float distance = Mathf.Round(Vector3.Distance(startPoint.position, endPoint.position));

        for (int i = 0; i < distance; i++)
        {
            if(spawnChance > Random.Range(0f,1f))
            {
                Vector3 spawningPosition = new Vector3
                    (startPoint.position.x - i, startPoint.position.y, startPoint.position.z);

                int index = Random.Range(0, grassPrefabs.Count);
                objectPool.OnSpawnObject(grassPrefabs[index].name, spawningPosition, Quaternion.identity, transform);
            }
        }
    }

    private void OnDisable()
    {
        canSpawn = false;
    }

    public void OnSpawn()
    {
        canSpawn = true;
        StartChunkFilling();
    }
}