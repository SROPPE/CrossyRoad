using System;
using UnityEngine;
using UnityEngine.Analytics;

public class CoinSpawner : MonoBehaviour, IPolledObject
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float spawnChance = 0.01f;

    private ObjectPool objectPool;

    public void OnSpawn()
    {
        StartSpawningAction();
    }

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
    }

    private void StartSpawningAction()
    {
        int levelBorders = (int)LevelBorders.rightBorderPosition.x;
        for (int i = -levelBorders; i < levelBorders; i++)
        {
            if (spawnChance > UnityEngine.Random.Range(0f,1f))
            {
                Vector3 spawnPosition = new Vector3(transform.position.x + i, transform.position.y, transform.position.z);
                if (CheckForObstacle(spawnPosition)) continue;
                objectPool.OnSpawnObject(coinPrefab.name, spawnPosition,Quaternion.identity,transform);
            }
        }
    }

    private bool CheckForObstacle(Vector3 position)
    {
        Vector3 OR = new Vector3(position.x / 2, position.y + 20, position.z / 2);
        Vector3 POS = new Vector3(position.x / 2, position.y - 20, position.z / 2);
        RaycastHit hit;
        if (Physics.Raycast(OR, POS, out hit))
        {

            return hit.collider.CompareTag("Obstacle");
        }
        return false;
    }
}
