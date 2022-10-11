using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mesh;

public class MeshData
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uv = new List<Vector2>();

    private List<Vector3> colliderVertices = new List<Vector3>();
    private List<int> colliderTriangles = new List<int>();

    public MeshData()
    {

    }

    public void AddVertex(Vector3 vertex, bool generateCollider)
    {
        vertices.Add(vertex);

        if (generateCollider)
            colliderVertices.Add(vertex);
    }

    public void AddQuadIndexes(bool generateCollider)
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        if (generateCollider)
        {
            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 3);
            colliderTriangles.Add(colliderVertices.Count - 2);

            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 1);
        }
    }

    public void AddUvs(Vector2[] uvs)
    {
        uv.AddRange(uvs);
    }

    public void BuildMesh(Mesh mesh)
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();

        mesh.RecalculateNormals();
    }
}
