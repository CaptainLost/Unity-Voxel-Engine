using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Chunk
{
    private static Vector3Int GetPostitionFromIndex(ChunkData chunkData, int index)
    {
        int x = index % chunkData.ChunkSize;
        int y = (index / chunkData.ChunkSize) % chunkData.ChunkHeight;
        int z = index / (chunkData.ChunkSize * chunkData.ChunkHeight);

        return new Vector3Int(x, y, z);
    }

    public static void LoopThroughTheBlocks(ChunkData chunkData, Action<Vector3Int> actionToPerform)
    {
        for (int index = 0; index < chunkData.Blocks.Length; index++)
        {
            Vector3Int position = GetPostitionFromIndex(chunkData, index);

            actionToPerform(position);
        }
    }

    // In chunk coordinate system
    private static bool InRange(ChunkData chunkData, int axisCoordinate)
    {
        if (axisCoordinate < 0 || axisCoordinate >= chunkData.ChunkSize)
            return false;

        return true;
    }

    // In chunk coordinate system
    private static bool InRangeHeight(ChunkData chunkData, int ycoordinate)
    {
        if (ycoordinate < 0 || ycoordinate >= chunkData.ChunkHeight)
            return false;

        return true;
    }

    public static int GetIndexFromPosition(ChunkData chunkData, int x, int y, int z)
    {
        return x + chunkData.ChunkSize * y + chunkData.ChunkSize * chunkData.ChunkHeight * z;
    }

    public static void SetBlock(ChunkData chunkData, Vector3Int localPosition, BlockType block)
    {
        if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z))
        {
            int index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);

            chunkData.Blocks[index] = block;
        }
        else
        {
            throw new Exception("Need to ask World for appropiate chunk");
        }
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
    {
        if (InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z))
        {
            int index = GetIndexFromPosition(chunkData, x, y, z);
            return chunkData.Blocks[index];
        }

        return chunkData.World.GetBlockFromChunkCoordinates(chunkData,
            chunkData.ChunkPosition.WorldPosition.x + x,
            chunkData.ChunkPosition.WorldPosition.y + y,
            chunkData.ChunkPosition.WorldPosition.z + z);
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int pos)
    {
        return GetBlockFromChunkCoordinates(chunkData, pos.x, pos.y, pos.z);
    }

    public static Vector3Int ChunkPositionFromBlockCoords(World world, int x, int y, int z)
    {
        Vector3Int pos = new Vector3Int
        {
            x = Mathf.FloorToInt(x / (float)world.ChunkSize) * world.ChunkSize,
            y = Mathf.FloorToInt(y / (float)world.ChunkHeight) * world.ChunkHeight,
            z = Mathf.FloorToInt(z / (float)world.ChunkSize) * world.ChunkSize
        };
        return pos;
    }

    public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int pos)
    {
        return new Vector3Int
        {
            x = pos.x - chunkData.ChunkPosition.WorldPosition.x,
            y = pos.y - chunkData.ChunkPosition.WorldPosition.y,
            z = pos.z - chunkData.ChunkPosition.WorldPosition.z
        };
    }
}