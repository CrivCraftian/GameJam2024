using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int Position { get; private set; }

    public int sizeX { get; private set; }
    public int sizeY { get; private set; }

    public Dictionary<Room, float> connectedRooms;

    public Room(Vector2Int position, int sizeX, int sizeY)
    {
        this.Position = position;

        this.sizeX = sizeX;
        this.sizeY = sizeY;

        connectedRooms = new Dictionary<Room, float>();
    }

    public void AddConnection(Room room, float distance)
    {
        if(connectedRooms.ContainsKey(room))
        {
            return;
        }

        connectedRooms.Add(room, distance);
    }
}
