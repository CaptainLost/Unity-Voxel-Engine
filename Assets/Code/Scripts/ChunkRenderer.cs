using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private Mesh mesh;

    private ChunkData chunkData;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        mesh = meshFilter.mesh;
    }

    private void Start()
    {
        //MeshData meshData = MeshDataBuilder.BuildMeshData(new ChunkData());

        //SetMesh(meshData);

    }

    public void Initialize(ChunkData chunkData)
    {
        this.chunkData = chunkData;
    }

    public void UpdateChunk(MeshData meshData)
    {
        meshData.BuildMesh(mesh);
    }
}
