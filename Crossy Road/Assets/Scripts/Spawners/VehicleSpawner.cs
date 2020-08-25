using CrossyRoad.Obstacles;
using System;
using UnityEngine;

namespace CrossyRoad.Spawners
{
    public class VehicleSpawner : MonoBehaviour, IPolledObject
    {
        [Header("Spawning Vehicle Settings")]
        [SerializeField] private GameObject vehiclePrefab;
        [SerializeField] private float minVehicleMovementDuration = 2f;
        [SerializeField] private float maxVehicleMovementDuration = 5f;

        [Header("Spawner Settings")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private float spawnRate = 3f;

        private ObjectPool objectPool;

        private float currentGivenSpeed;                        //Changes every time a chunk appears
        private float timeScinceLastSpawn;    

        private bool canSpawn = false;
        private bool IsRightLeft = false;                       //True if the objects that spawned move from right to left
        private void Start()
        {
            objectPool = ObjectPool.Instance;
        }
        private void OnEnable()
        {
            CalculateRandomValues();
        }
        private void OnDisable()
        {
            canSpawn = false;
        }

        private void Update()
        {
            StartSpawnAction();
        }

        private void StartSpawnAction()
        {
            if (!canSpawn) return;

            if (timeScinceLastSpawn <= spawnRate)
            {
                timeScinceLastSpawn += Time.deltaTime;
                return;
            }

            timeScinceLastSpawn = 0f;
            var vehicleInstance = objectPool.OnSpawnObject(vehiclePrefab.name, spawnPoint.position, Quaternion.identity, transform);

            if (vehicleInstance != null)
            {
                vehicleInstance.GetComponent<IMoveableObstacle>().StartMoveAction(endPoint.position, currentGivenSpeed);
            }
        }
        private void CalculateRandomValues()
        {
            TrySwapPoints();
            currentGivenSpeed = UnityEngine.Random.Range(minVehicleMovementDuration, maxVehicleMovementDuration);
        }
        private void TrySwapPoints()
        {
            IsRightLeft = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
            if (IsRightLeft)
            {
                Vector3 buffer = endPoint.position;
                endPoint.position = spawnPoint.position;
                spawnPoint.position = buffer;
            }
        }

        public void OnSpawn()
        {
            timeScinceLastSpawn = Mathf.Infinity;
            canSpawn = true;
        }
    }
}