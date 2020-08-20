using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour, IPolledObject
{
    [Header("Spawning Vehicle Settings")]
    [SerializeField] private GameObject vehiclePrefab;
    [SerializeField] private float minVehicleSpeed = 2f;
    [SerializeField] private float maxVehicleSpeed = 5f;

    [Header("Spawner Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float spawnRate = 6f;

    private float timeScinceLastSpawn = Mathf.Infinity;
    private float currentGivenSpeed;
    private bool canSpawn = false;
    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = ObjectPool.Instance;
    }
    private void OnEnable()
    {
        currentGivenSpeed = Random.Range(minVehicleSpeed, maxVehicleSpeed);
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
        var vehicleInstance = objectPool.OnSpawnObject(vehiclePrefab.name, spawnPoint.position, Quaternion.identity, null);
     
        vehicleInstance.GetComponent<Log>().StartMoveAction(spawnPoint.position,endPoint.position, currentGivenSpeed);
    }

    public void OnSpawn()
    {
        canSpawn = true;
    }
}
