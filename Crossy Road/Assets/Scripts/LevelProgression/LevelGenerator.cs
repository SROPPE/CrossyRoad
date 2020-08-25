using CrossyRoad.Chunks;
using CrossyRoad.Core;
using System.Collections.Generic;
using UnityEngine;

namespace CrossyRoad.LevelProgression
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Spawning Chunks Settigns")]
        [SerializeField] private List<ChunkData> chunks;    //List of chunks prefabs
        [SerializeField] private Vector3 currentSpawningPosition = Vector3.zero; 
        [SerializeField] private int maxChunksCapacity;     //Maximum number of displayed chunks   
        [SerializeField] private int startingSpotLength;   
        [Header("Borders Settings")]
        [SerializeField] private GameObject border;         //Border prefab
        [SerializeField] private int spawnBorderEveryChuncks = 100;

        private ObjectPool objectPool;
        private List<GameObject> currentField;              //List of visible chunks
        private SpawningChunk currentSpawningChunk;         //Contains chunk info
        private int spawnedChunks;                          //Number of spawned chunks    
        private Vector3 defaultSpawningPosition;            //Contains the original value of currentSpawningPosition

        private PlayerMovement playerMovement;              


        bool isInitialSpawning = true;                      //True if the number of spawned chunks < maxChunksCapacity
        bool isStartSpotSpawning = true;                    //True if the number of spawned chunks < startingSpotLength

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
            playerMovement.onGetNewZPosition.AddListener(StartChunkSpawnigAction);
        }
        private void OnDisable()
        {
            playerMovement.onGetNewZPosition.RemoveListener(StartChunkSpawnigAction);
        }
        public void StartNewLevelGeneration()   //GenerateNewLevel
        {
            currentField.Clear();
            isInitialSpawning = true;
            isStartSpotSpawning = true;
            spawnedChunks = spawnBorderEveryChuncks;
            currentSpawningChunk.Reset();
            currentSpawningPosition = defaultSpawningPosition;

            InitialFieldCreation();
        }
        public void StartChunkSpawnigAction()   //SpawnNewChunk
        {
            SelectNewChunk();

            SpawnChunk(chunks[currentSpawningChunk.Index]);
        }
        private void InitialFieldCreation()
        {
            for (int i = 0; i < maxChunksCapacity; i++)
            {
                StartChunkSpawnigAction();
            }
            isInitialSpawning = false;
        }
        private void SpawnChunk(ChunkData chunk)
        {

            var instance = objectPool.OnSpawnObject(chunk.GetChunkPrefab().name, currentSpawningPosition, Quaternion.identity, transform);

            UpdateChangeableVariables(instance);

            TrySpawnNewBorders();

            HideOldestChunk();
        }
        private void UpdateChangeableVariables(GameObject instance)
        {
            currentField.Add(instance);
            currentSpawningPosition.z++;
            currentSpawningChunk.SpawnedLength++;
            spawnedChunks++;
        }
        private void TrySpawnNewBorders()
        {
            if (spawnBorderEveryChuncks <= spawnedChunks + maxChunksCapacity)
            {
                Vector3 spawningPosition;
                if (!isInitialSpawning)
                {
                    spawningPosition = currentSpawningPosition + new Vector3(0, 0, maxChunksCapacity);
                }
                else
                {
                    spawningPosition = currentSpawningPosition;
                }

                objectPool.OnSpawnObject(border.name, spawningPosition, Quaternion.identity, transform);
                spawnedChunks = 0;
            }
        }
        private void HideOldestChunk()
        {
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
                currentSpawningChunk.Index = Random.Range(0, chunks.Count);
                var chunk = chunks[currentSpawningChunk.Index];
                currentSpawningChunk.Length = chunk.GetChunkLength();
            }
        }


    }
}