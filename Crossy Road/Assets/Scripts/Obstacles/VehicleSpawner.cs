using System;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour, IPolledObject
{
    [Header("Spawning Vehicle Settings")]
    [SerializeField] private GameObject vehiclePrefab;
    [SerializeField] private float minVehicleSpeed = 2f;
    [SerializeField] private float maxVehicleSpeed = 5f;

    [Header("Spawner Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float spawnRate = 3f;

    private float timeScinceLastSpawn = Mathf.Infinity;
    private float currentGivenSpeed;
    private bool canSpawn = false;
    private ObjectPool objectPool;
    private bool IsRightLeft = false;
    private void Start()
    {
        objectPool = ObjectPool.Instance;
    }
    private void OnEnable()
    {
        CalculateRandomValues();
    }

    private void CalculateRandomValues()
    {
        TrySwapPoints();
        currentGivenSpeed = UnityEngine.Random.Range(minVehicleSpeed, maxVehicleSpeed);
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

        vehicleInstance.GetComponent<IMoveable>().StartMoveAction(endPoint.position, currentGivenSpeed);
    }

    public void OnSpawn()
    {
        canSpawn = true;
    }
}
