using System.Collections.Generic;
using UnityEngine;

namespace CrossyRoad.Chunks
{
    [CreateAssetMenu(fileName = "Chunk", menuName = "LevelGenerator/Create New Chunk")]
    public class ChunkData : ScriptableObject
    {
        [SerializeField] private List<GameObject> chunkPrefabs;
        [SerializeField] private int minChunkLength;
        [SerializeField] private int maxChunkLength;

        public GameObject GetChunkPrefab() => chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];
        public int GetChunkLength() => Random.Range(minChunkLength, maxChunkLength);

    }
}