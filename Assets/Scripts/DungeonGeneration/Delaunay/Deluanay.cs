using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Triangle
{
    public Vector2 p1, p2, p3;
    public HalfEdge[] edges;

    public Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        this.p1 = p1; this.p2 = p2; this.p3 = p3;

        edges = new HalfEdge[3] { new HalfEdge(p1, p2), new HalfEdge(p2, p3), new HalfEdge(p3, p1) };
    }
}

public struct HalfEdge
{
    public Vector2 p1, p2;

    public HalfEdge(Vector2 p1, Vector2 p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }

    public static bool isEqual(HalfEdge e1, HalfEdge e2)
    {
        if(e1.p1 == e2.p1 && e1.p2 == e2.p2 || e1.p1 == e2.p2 && e1.p2 == e2.p1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public static class Deluanay
{
    public static Triangle[] Triangulate(Vector2Int[] Points, Triangle supraTriangle)
    {
        HashSet<Triangle> Triangles = new HashSet<Triangle>() { supraTriangle };

        foreach(Vector2Int point in Points)
        {
            List<Triangle> badTriangles = new List<Triangle>();
            List<HalfEdge> polygon = new List<HalfEdge>();
            List<HalfEdge> currentEdges = new List<HalfEdge>();

            foreach (Triangle triangle in Triangles)
            {
                Vector2 circumCenter = CalculateCircumcircleCenter(triangle);
                float circumRadius = CalculateCircumcircleRadius(circumCenter, triangle.p1);

                if(Vector2.Distance(circumCenter, point) < circumRadius)
                {
                    badTriangles.Add(triangle);
                }
            }

            foreach(Triangle badTriangle in badTriangles)
            {
                currentEdges.AddRange(badTriangle.edges);
            }

            foreach(Triangle badTriangle in badTriangles)
            {
                foreach(HalfEdge badEdge in badTriangle.edges)
                {
                    int appearanceCount = 0;

                    foreach(HalfEdge edge in currentEdges)
                    {
                        if(HalfEdge.isEqual(edge, badEdge))
                        {
                            appearanceCount++;
                        }
                    }

                    if(appearanceCount < 2)
                    {
                        polygon.Add(badEdge);
                    }
                }
            }

            foreach(HalfEdge edges in polygon)
            {
                Triangles.Add(new Triangle(edges.p1, point, edges.p2));
            }

            foreach(Triangle badTriangle in badTriangles)
            {
                Triangles.Remove(badTriangle);
            }
        }

        List<Triangle> trianglesToRemove = new List<Triangle>();

        foreach(Triangle triangle in Triangles)
        {
            if(triangle.p1 == supraTriangle.p1 || triangle.p2 == supraTriangle.p1 || triangle.p3 == supraTriangle.p1)
            {
                trianglesToRemove.Add(triangle);
            }
            if (triangle.p1 == supraTriangle.p2 || triangle.p2 == supraTriangle.p2 || triangle.p3 == supraTriangle.p2)
            {
                trianglesToRemove.Add(triangle);
            }
            if (triangle.p1 == supraTriangle.p3 || triangle.p1 == supraTriangle.p3 || triangle.p3 == supraTriangle.p3)
            {
                trianglesToRemove.Add(triangle);
            }
        }

        foreach (Triangle triangle in trianglesToRemove)
        {
            Triangles.Remove(triangle);
        }


        Triangle[] newArray = new Triangle[Triangles.Count];
        Triangles.CopyTo(newArray, 0)
            ;
        return newArray;
    }

    public static Triangle GenerateSupraTriangle(int x, int y)
    {
        Vector2Int p1, p2, p3;

        p1 = new Vector2Int(x-x*2, 0);
        p2 = new Vector2Int(x/2, y * 2);
        p3 = new Vector2Int(x * 2, 0);

        return new Triangle(p1, p2, p3);
    }

    private static Vector2 CalculateCircumcircleCenter(Triangle triangle)
    {
        float x1 = triangle.p1.x;
        float y1 = triangle.p1.y;
        float x2 = triangle.p2.x;
        float y2 = triangle.p2.y;
        float x3 = triangle.p3.x;
        float y3 = triangle.p3.y;

        float D = 2 * (x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));

        float Ux = ((x1 * x1 + y1 * y1) * (y2 - y3) + (x2 * x2 + y2 * y2) * (y3 - y1) + (x3 * x3 + y3 * y3) * (y1 - y2)) / D;

        float Uy = ((x1 * x1 + y1 * y1) * (x3 - x2) + (x2 * x2 + y2 * y2) * (x1 - x3) + (x3 * x3 + y3 * y3) * (x2 - x1)) / D;

        return new Vector2((float)Ux, (float)Uy);
    }

    private static float CalculateCircumcircleRadius(Vector2 point, Vector2 triPoint)
    {
        return Vector2.Distance(point, triPoint);
    }
}
