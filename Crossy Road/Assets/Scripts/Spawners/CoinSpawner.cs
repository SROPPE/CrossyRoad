using CrossyRoad.Obstacles;
using UnityEngine;
namespace CrossyRoad.Spawners
{
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
                if (spawnChance > Random.Range(0f, 1f))
                {
                    Vector3 spawnPosition = new Vector3(transform.position.x + i, transform.position.y, transform.position.z);

                    if (RaycastPositionChecker.Check(spawnPosition, "Obstacle")) continue;

                    objectPool.OnSpawnObject(coinPrefab.name, spawnPosition, Quaternion.identity, transform);
                }
            }
        }


    }
}