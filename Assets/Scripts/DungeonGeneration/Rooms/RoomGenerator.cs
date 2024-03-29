using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomGenerator
{
    public static Room[] GenerateRooms(Grid grid, int count, int lowerSizeX, int lowerSizeY, int largerSizeX, int largerSizeY)
    {
        List<Room> rooms = new List<Room>();

        HashSet<Vector2Int> Positions = new HashSet<Vector2Int>();

        for (int i = 0; i < count; i++)
        {
            bool isValid = false;
            while(!isValid)
            {
                Vector2Int tmpPosition = new Vector2Int(Random.Range(largerSizeX, grid.gridX - largerSizeX), Random.Range(largerSizeY, grid.gridY - largerSizeY));

                isValid = true;

                foreach(Room room in rooms)
                {
                    float distanceBetweenPoints = Vector2.Distance(tmpPosition, room.Position);
                    if (distanceBetweenPoints < 5)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    Positions.Add(tmpPosition);
                }
            }
        }

        foreach(Vector2Int position in Positions)
        {
            rooms.Add(new Room(position, Random.Range(lowerSizeX, largerSizeX), Random.Range(lowerSizeY, largerSizeY)));
        }

        return rooms.ToArray();
    }

    public static Vector2Int[] RoomsToPoints(Room[] rooms)
    {
        List<Vector2Int> points = new List<Vector2Int>();

        foreach(Room room in rooms)
        {
            points.Add(room.Position);
        }

        return points.ToArray();
    }
}
