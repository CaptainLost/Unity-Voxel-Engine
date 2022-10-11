using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPosition
{
    private Vector3Int worldPosition;

    public Vector3Int WorldPosition { get { return worldPosition; } }

    public ChunkPosition(Vector3Int worldPosition)
    {
        this.worldPosition = worldPosition;
    }

    public Vector3Int ChunkPositionFromBlockCoords(World world, int x, int y, int z)
    {
        Vector3Int pos = new Vector3Int
        {
            x = Mathf.FloorToInt(x / (float)world.ChunkSize) * world.ChunkSize,
            y = Mathf.FloorToInt(y / (float)world.ChunkHeight) * world.ChunkHeight,
            z = Mathf.FloorToInt(z / (float)world.ChunkSize) * world.ChunkSize
        };
        return pos;
    }
}
