using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prims
{
    public static Dictionary<Room, List<Room>> RunPrims(Room[] rooms)
    {
        HashSet<Room> visitedRooms = new HashSet<Room>();

        Dictionary<Room, List<Room>> MST = new Dictionary<Room, List<Room>>();

        Stack<Room> roomStack = new Stack<Room>();
        roomStack.Push(rooms[0]);

        while(visitedRooms.Count < rooms.Length)
        {
            Room currentRoom = roomStack.Pop();

            if (currentRoom == null) break;

            float minimumValue = Mathf.Infinity;

            Room bestRoomChoice = null;

            visitedRooms.Add(currentRoom);

            foreach(KeyValuePair<Room, float> connectedRoom in currentRoom.connectedRooms)
            {
                if(visitedRooms.Contains(connectedRoom.Key))
                {
                    continue;
                }
                else if(connectedRoom.Value < minimumValue)
                {
                    minimumValue = connectedRoom.Value;
                    bestRoomChoice = connectedRoom.Key;
                }
            }

            if (bestRoomChoice != null)
            {
                if (MST.ContainsKey(currentRoom))
                {
                    MST[currentRoom].Add(bestRoomChoice);
                }
                else
                {
                    MST.Add(currentRoom, new List<Room>() { bestRoomChoice });
                }

                roomStack.Push(currentRoom);
                roomStack.Push(bestRoomChoice);
            }
        }

        return MST;
    }
}
