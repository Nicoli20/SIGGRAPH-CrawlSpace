using System.Collections.Generic;
using UnityEngine;

// Creates and manages the mesh of the grid.
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh hexMesh; // The grid's mesh.
    List<Vector3> vertices; // The mesh's vertices.
    List<int> triangles; // The mesh's triangles.
    List<Color> colors; // The mesh's colors.
    MeshCollider meshCollider; // The collider for the mesh.

    // Runs during game initialization, setting the global variables.
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        colors = new List<Color>();
        triangles = new List<int>();
    }

    // Triangulates each cell and sets the normals of the mesh.
    public void Triangulate(HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        colors.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
        meshCollider.sharedMesh = hexMesh;
    }

    // Triangulates a hexcell. A hexagon is made of six triangles.
    private void Triangulate (HexCell cell)
    {
        Vector3 center = cell.transform.localPosition; // The "7th" vertex.
        for (int i = 0; i < 6; i++)
        {
            AddTriangle
            (center,
            center + HexMetrics.corners[i],
            center + HexMetrics.corners[i + 1]);
            AddTriangleColor(cell.color);
        } 
    }

    // Colors the triangle.
    private void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    // Creates a triangle using vertices.
    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
}
