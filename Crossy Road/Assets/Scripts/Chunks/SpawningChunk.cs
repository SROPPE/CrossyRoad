using UnityEngine;

namespace CrossyRoad.Chunks
{
    public class SpawningChunk : ScriptableObject
    {
        public int Index { get; set; }          //Index in prefabs list
        public int Length { get; set; }         //Length of chunk
        public int SpawnedLength { get; set; }  //Number of spawned pieces of chunk

        public void Reset()
        {
            Index = 0;
            Length = 0;
            SpawnedLength = 0;
        }

    }
}