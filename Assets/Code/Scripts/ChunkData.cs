using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData
{
    private BlockType[] blocks;

    private ChunkPosition chunkPosition;
    private int chunkSize;
    private int chunkHeight;

    private World world;

    public BlockType[] Blocks { get { return blocks; } }
    public ChunkPosition ChunkPosition { get { return chunkPosition; } }
    public int ChunkSize { get { return chunkSize; } }
    public int ChunkHeight { get { return chunkHeight; } }
    public World World { get { return world; } }

    public ChunkData(World world, int chunkSize, int chunkHeight, Vector3Int chunkPosition)
    {
        this.world = world;
        this.chunkSize = chunkSize;
        this.chunkHeight = chunkHeight;

        this.chunkPosition = new ChunkPosition(chunkPosition);
        this.blocks = new BlockType[chunkSize * chunkHeight * chunkSize];
    }
}
