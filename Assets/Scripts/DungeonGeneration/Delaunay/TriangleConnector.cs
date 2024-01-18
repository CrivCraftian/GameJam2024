using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TriangleConnector
{
    public static void TrianglesToRooms(Triangle[] triangles, Room[] rooms)
    {
        Dictionary<Vector2, Room> roomPositions = new Dictionary<Vector2, Room>();

        foreach(Room room in rooms)
        {
            /*
            if(roomPositions.ContainsKey(room.Position))
            {
                continue;
            }
            */

            roomPositions.Add(room.Position, room);
        }

        foreach(Triangle triangle in triangles)
        {
            foreach(HalfEdge edge in triangle.edges)
            {
                float distance = Vector2.Distance(edge.p1, edge.p2);

                Room tmpRoom1 = roomPositions[edge.p1];

                Room tmpRoom2 = roomPositions[edge.p2];

                tmpRoom1.AddConnection(tmpRoom2, distance);
            }
        }
    }
}
