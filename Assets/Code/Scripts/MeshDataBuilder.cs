using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public static class MeshDataBuilder
{
    private static Direction[] directions =
    {
        Direction.backwards,
        Direction.down,
        Direction.foreward,
        Direction.left,
        Direction.right,
        Direction.up
    };

    public static MeshData BuildMeshData(ChunkData chunkData)
    {
        MeshData meshData = new MeshData();

        Chunk.LoopThroughTheBlocks(chunkData, (position) => BuildBlock(chunkData, meshData, position, chunkData.Blocks[Chunk.GetIndexFromPosition(chunkData, position.x, position.y, position.z)]));

        return meshData;
    }

    public static void BuildBlock(ChunkData chunkData, MeshData meshData, Vector3Int position, BlockType blockType)
    {
        if (blockType == BlockType.Air || blockType == BlockType.Nothing)
            return;

        bool generateColiider = BlockDataManager.instance.BlockDataDictionary[blockType].generateColiider;

        foreach (Direction direction in directions)
        {
            Vector3Int neighbourBlockPostion = position + direction.GetVector();
            BlockType neighbourBlockType = Chunk.GetBlockFromChunkCoordinates(chunkData, neighbourBlockPostion);

            //Debug.Log(neighbourBlockType);
            if (BlockDataManager.instance.BlockDataDictionary[neighbourBlockType].isSolid == false)
            {
                SetFaceData(meshData, direction, position, generateColiider);
                SetUvs(meshData, blockType);
            }
        }
    }

    public static void SetFaceData(MeshData meshData, Direction direction, Vector3Int position, bool generateCollider)
    {
        switch (direction)
        {
            case Direction.backwards:
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y - 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y + 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y + 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y - 0.5f, position.z - 0.5f), generateCollider);

                break;
            case Direction.foreward:
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y - 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y + 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y + 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y - 0.5f, position.z + 0.5f), generateCollider);

                break;
            case Direction.left:
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y - 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y + 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y + 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y - 0.5f, position.z - 0.5f), generateCollider);

                break;

            case Direction.right:
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y - 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y + 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y + 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y - 0.5f, position.z + 0.5f), generateCollider);

                break;
            case Direction.down:
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y - 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y - 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y - 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y - 0.5f, position.z + 0.5f), generateCollider);

                break;
            case Direction.up:
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y + 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y + 0.5f, position.z + 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x + 0.5f, position.y + 0.5f, position.z - 0.5f), generateCollider);
                meshData.AddVertex(new Vector3(position.x - 0.5f, position.y + 0.5f, position.z - 0.5f), generateCollider);

                break;
            default:
                return;
        }

        meshData.AddQuadIndexes(generateCollider);
    }

    public static void SetUvs(MeshData meshData, BlockType blockType)
    {
        Vector2[] uvs = new Vector2[4];

        Vector2Int tilePos = BlockDataManager.instance.BlockDataDictionary[blockType].texture;

        uvs[0] = new Vector2(BlockDataManager.instance.TileSizeX * tilePos.x + BlockDataManager.instance.TileSizeX - BlockDataManager.instance.TextureOffset,
            BlockDataManager.instance.TileSizeX * tilePos.y + BlockDataManager.instance.TextureOffset);

        uvs[1] = new Vector2(BlockDataManager.instance.TileSizeX * tilePos.x + BlockDataManager.instance.TileSizeX - BlockDataManager.instance.TextureOffset,
            BlockDataManager.instance.TileSizeX * tilePos.y + BlockDataManager.instance.TileSizeX - BlockDataManager.instance.TextureOffset);

        uvs[2] = new Vector2(BlockDataManager.instance.TileSizeX * tilePos.x + BlockDataManager.instance.TextureOffset,
            BlockDataManager.instance.TileSizeX * tilePos.y + BlockDataManager.instance.TileSizeX - BlockDataManager.instance.TextureOffset);

        uvs[3] = new Vector2(BlockDataManager.instance.TileSizeX * tilePos.x + BlockDataManager.instance.TextureOffset,
            BlockDataManager.instance.TileSizeX * tilePos.y + BlockDataManager.instance.TextureOffset);

        meshData.AddUvs(uvs);
    }
}
