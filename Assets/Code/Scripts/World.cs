using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private int mapSizeInChunks = 6;
    [SerializeField] private int chunkSize = 16, chunkHeight = 100;
    [SerializeField] private GameObject chunkPrefab;

    private Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();

    public int ChunkSize { get { return chunkSize; } }
    public int ChunkHeight { get { return chunkHeight; } }

    private void Start()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        for (int x = 0; x < mapSizeInChunks; x++)
        {
            for (int z = 0; z < mapSizeInChunks; z++)
            {
                ChunkData data = new ChunkData(this, chunkSize, chunkHeight, new Vector3Int(x * chunkSize, 0, z * chunkSize));

                // Generate World Here
                GenerateChunk(data);

                chunkDataDictionary.Add(data.ChunkPosition.WorldPosition, data);
            }
        }

        foreach (ChunkData chunkData in chunkDataDictionary.Values)
        {
            GameObject chunk = Instantiate(chunkPrefab, chunkData.ChunkPosition.WorldPosition, Quaternion.identity, transform);

            ChunkRenderer chunkRenderer = chunk.GetComponent<ChunkRenderer>();
            chunkRenderer.Initialize(chunkData);

            MeshData meshData = MeshDataBuilder.BuildMeshData(chunkData);
            chunkRenderer.UpdateChunk(meshData);
        }
    }

    private void GenerateChunk(ChunkData data)
    {
        for (int x = 0; x < data.ChunkSize; x++)
        {
            for (int z = 0; z < data.ChunkSize; z++)
            {
                for (int y = 0; y < chunkHeight; y++)
                {
                    BlockType blockType = BlockType.Solid;

                    if (y == 5)
                    {
                        blockType = BlockType.Solid;
                    }

                    Chunk.SetBlock(data, new Vector3Int(x, y, z), blockType);
                }
            }
        }
    }

    public BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
    {
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);
        ChunkData containerChunk = null;

        chunkDataDictionary.TryGetValue(pos, out containerChunk);

        if (containerChunk == null)
            return BlockType.Nothing;

        Vector3Int blockInCHunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));

        return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInCHunkCoordinates);
    }
}
