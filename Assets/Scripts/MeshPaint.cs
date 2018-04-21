using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class MeshPaint : MonoBehaviour
{
    public Color color = Color.white;
    public Color vertexColor = Color.white;
    public float interpoleVertexColorLerp = 0.5F;
    public List<Vector3> points = new List<Vector3>();
    public int pointsCount;

    private MeshFilter meshFilter;
    private Mesh mesh;
    private MeshCollider meshCollider;

    public bool DEBUG = false;

    void Start()
    {
        GetComponent<Renderer>().material.color = color;
    }

    private void OnValidate()
    {
        pointsCount = points.Count;

        if (!meshFilter)
            meshFilter = GetComponent<MeshFilter>();

        if (!meshCollider)
            meshCollider = GetComponent<MeshCollider>();
    }

    public void AddPoint(Vector3 position)
    {
        points.Add(position);
        pointsCount = points.Count;
    }

    public void Triangulate()
    {
        if (this.points.Count < 3)
        {
            Reset();
            return;
        }

        meshFilter.mesh = null;
        mesh = null;
        meshCollider.sharedMesh = null;

        List<Vector3> points = new List<Vector3>();
        foreach (Vector3 point in this.points)
            points.Add(point);


        Vector3 centerPoint = Vector3.zero;
        foreach (Vector3 point in points)
        {
            centerPoint += point;
        }
        centerPoint /= points.Count;

        Vector3 offset = transform.position + centerPoint;
        Vector3 direction = Vector3.zero - centerPoint;
        centerPoint = offset + direction - transform.position;
        for (int i = 0; i < points.Count; ++i)
        {
            points[i] = (points[i] + centerPoint) + direction;
        }

        points.Add(points[0]);
        
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Color> colors = new List<Color>();

        Vector3 interpole1 = Vector3.zero;
        Vector3 interpole2 = Vector3.zero;
        Vector3 interpole3 = Vector3.zero;
        Vector3 interpole4 = Vector3.zero;

        for (int i = 0; i < points.Count - 1; ++i)
        {
            interpole1 = Vector3.Lerp(centerPoint, points[i], 0.9F);
            interpole2 = Vector3.Lerp(centerPoint, points[i + 1], 0.9F);
            interpole3 = Vector3.Lerp(centerPoint, interpole1, 0.5F);
            interpole4 = Vector3.Lerp(centerPoint, interpole2, 0.5F);

            AddTriangle(centerPoint, interpole3, interpole4, ref vertices, ref triangles);
            Color interpoleColor34 = Color.black;
            AddTriangleColor(Color.black, interpoleColor34, interpoleColor34, ref colors);

            AddQuad(interpole3, interpole4, interpole2, interpole1, ref vertices, ref triangles);
            Color interpoleColor3412 = Color.Lerp(interpoleColor34, vertexColor, interpoleVertexColorLerp);
            AddTriangleColor(interpoleColor34, interpoleColor3412, interpoleColor34, ref colors);
            AddTriangleColor(interpoleColor34, interpoleColor3412, interpoleColor3412, ref colors);

            AddQuad(interpole1, interpole2, points[i + 1], points[i], ref vertices, ref triangles);
            Color cornerColor = Color.Lerp(interpoleColor3412, Color.white, 0.9F);
            AddTriangleColor(interpoleColor3412, cornerColor, interpoleColor3412, ref colors);
            AddTriangleColor(interpoleColor3412, cornerColor, cornerColor, ref colors);
        }

        for (int i = 0; i < points.Count - 1; ++i)
        {
            AddQuad(points[i], points[i + 1], points[i + 1] + Vector3.forward, points[i] + Vector3.forward, ref vertices, ref triangles);
            AddTriangleColor(Color.white, ref colors);
            AddTriangleColor(Color.white, ref colors);
        }
        AddQuad(points[points.Count - 1], points[1], points[1] + Vector3.forward, points[points.Count - 1] + Vector3.forward, ref vertices, ref triangles);
        AddTriangleColor(Color.white, ref colors);
        AddTriangleColor(Color.white, ref colors);

        mesh = new Mesh();
        mesh.name = "mesh";
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetColors(colors);
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = meshFilter.sharedMesh;

    }

    public void Reset()
    {
        points.Clear();
        meshFilter.mesh = null;
        mesh = null;
        meshCollider.sharedMesh = null;
        pointsCount = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (!DEBUG)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.6F);

        if (this.points.Count > 0)
        {
            Vector3 center = Vector3.zero;
            List<Vector3> points = new List<Vector3>();
            foreach (Vector3 p in this.points)
                points.Add(p);

            foreach (Vector3 point in points)
            {
                center += point;
            }
            center /= points.Count;

            Gizmos.color = Color.black;
            foreach (Vector3 p in points)
                Gizmos.DrawCube(p, Vector3.one * 0.5F);

            Gizmos.color = Color.red;
            Vector3 offset = transform.position + center;
            Vector3 direction = Vector3.zero - center;
            center = offset + direction;
            Gizmos.DrawCube(center, Vector3.one * 0.5F);
            for (int i = 0; i < points.Count; ++i)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube((points[i] + center) + direction, Vector3.one * 0.5F);
            }
        }
    }

    public static void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3, ref List<Vector3> vertices, ref List<int> triangles)
    {
        int vindex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vindex);
        triangles.Add(vindex + 1);
        triangles.Add(vindex + 2);
    }

    public static void AddTriangleColor(Color c1, Color c2, Color c3, ref List<Color> colors)
    {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
    }

    public static void AddTriangleColor(Color color, ref List<Color> colors)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    public static void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, ref List<Vector3> vertices, ref List<int> triangles)
    {
        AddTriangle(v1, v4, v2, ref vertices, ref triangles);
        AddTriangle(v2, v4, v3, ref vertices, ref triangles);
    }
}