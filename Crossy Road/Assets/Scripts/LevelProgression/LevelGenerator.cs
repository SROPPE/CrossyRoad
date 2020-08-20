using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<ChunkData> chunks;
    [SerializeField] private int maxChunksCapacity;
    [SerializeField] private Transform chunksParent;
    [SerializeField] private Vector3 currentSpawningPosition = Vector3.zero;
    [SerializeField] private int startingSpotLength;
    [SerializeField] private int spawnBorderEveryChuncks = 100;
    [SerializeField] private GameObject border;

    private PlayerMovement playerMovement;
    private List<GameObject> currentField;
    private SpawningChunk currentSpawningChunk;
    private ObjectPool objectPool;
    private int spawnedChunks;
    private Vector3 defaultSpawningPosition;
    bool isInitialSpawning = true;
    bool isStartSpotSpawning = true;
    private void Awake()
    {
        currentSpawningChunk = new SpawningChunk();
        currentField = new List<GameObject>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        spawnedChunks = spawnBorderEveryChuncks;
        defaultSpawningPosition = currentSpawningPosition;
    }
    private void Start()
    {
        objectPool = ObjectPool.Instance;
        StartNewLevelGeneration();
    }
    private void OnEnable()
    {
        playerMovement.onMoveForward.AddListener(StartChunkSpawnigAction);
    }
    private void OnDisable()
    {
        playerMovement.onMoveForward.RemoveListener(StartChunkSpawnigAction);
    }
    private void InitialFieldCreation()
    {
        for (int i = 0; i < maxChunksCapacity; i++)
        {
            StartChunkSpawnigAction();
        }
        isInitialSpawning = false;
    }


    public void StartChunkSpawnigAction()
    {
        SelectNewChunk();
        SpawnChunk(chunks[currentSpawningChunk.Index]);

    }
    private void SpawnChunk(ChunkData chunk)
    {

        var instance = objectPool.OnSpawnObject(chunk.GetChunkPrefab().name, currentSpawningPosition, Quaternion.identity, transform);

        currentField.Add(instance);
        currentSpawningPosition.z++;
        currentSpawningChunk.SpawnedLength++;
        spawnedChunks++;
        if (spawnBorderEveryChuncks <= spawnedChunks)
        {
            objectPool.OnSpawnObject(border.name, currentSpawningPosition, Quaternion.identity, transform);
            spawnedChunks = 0;
        }
        if (!isInitialSpawning)
        {
            currentField[0].SetActive(false);
            currentField.RemoveAt(0);
        }
    }
    private void SelectNewChunk()
    {
        if (currentSpawningChunk.Length > currentSpawningChunk.SpawnedLength) return;
        currentSpawningChunk.SpawnedLength = 0;
        if (isStartSpotSpawning)
        {
            currentSpawningChunk.Index = 0;
            currentSpawningChunk.Length = startingSpotLength;
            isStartSpotSpawning = false;
        }
        else
        {
            currentSpawningChunk.Index = UnityEngine.Random.Range(0, chunks.Count);
            var chunk = chunks[currentSpawningChunk.Index];
            currentSpawningChunk.Length = chunk.GetChunkLength();
        }
    }

    public void StartNewLevelGeneration()
    {
        currentField.Clear();
        isInitialSpawning = true;
        isStartSpotSpawning = true;
        spawnedChunks = spawnBorderEveryChuncks;
        currentSpawningChunk.Reset();
        currentSpawningPosition = defaultSpawningPosition;
        InitialFieldCreation();
    }

}