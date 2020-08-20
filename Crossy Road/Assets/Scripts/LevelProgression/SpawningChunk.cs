using UnityEngine;

public class SpawningChunk : ScriptableObject
{
    public int Index { get; set; }
    public int Length { get; set; }
    public int SpawnedLength { get; set; }

    public void Reset()
    {
        Index = 0;
        Length = 0;
        SpawnedLength = 0;
    }

}
