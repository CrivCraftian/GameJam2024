using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonController : MonoBehaviour
{
    [Header("Tilemap/Tiles")]
    [SerializeField] Tilemap floor;
    [SerializeField] Tile wallTile;

    [Header("Dungeon Size")]
    [SerializeField]int dungeonSizeX;
    [SerializeField]int dungeonSizeY;

    [Header("Rooms")]
    [SerializeField] int roomCount;

    [Header("Triangles")]
    [SerializeField] GameObject TrianglePrefab;

    [Header("Edges")]
    [SerializeField] GameObject EdgePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Grid grid = new Grid(dungeonSizeX, dungeonSizeY);

        Room[] rooms = RoomGenerator.GenerateRooms(grid, roomCount);
        Vector2Int[] roomPoints = RoomGenerator.RoomsToPoints(rooms);

        foreach (Room room in rooms)
        {
            grid.cells[room.Position.x, room.Position.y] = new Cell(CellType.Floor);
        }

        Triangle supraTriangle = Deluanay.GenerateSupraTriangle(grid.gridX, grid.gridY);

        Triangle[] triangles = Deluanay.Triangulate(roomPoints, supraTriangle);

        /*
        foreach (Triangle triangle in triangles)
        {
            GameObject currentTriangle = Instantiate(TrianglePrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);

            TriangleObject triScript = currentTriangle.GetComponent<TriangleObject>();

            triScript.tri = triangle;
        }
        */

        TriangleConnector.TrianglesToRooms(triangles, rooms);

        Dictionary<Room, List<Room>> MST = Prims.RunPrims(rooms);

        /*
        foreach (KeyValuePair<Room, List<Room>> primRoom in MST)
        {
            foreach(Room room in primRoom.Value)
            {
                GameObject edge = Instantiate(EdgePrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);

                EdgeObject edgeScript = edge.GetComponent<EdgeObject>();

                edgeScript.p1 = primRoom.Key.Position;
                edgeScript.p2 = room.Position;
            }
        }
        */

        foreach(KeyValuePair<Room, List<Room>> roomConnection in MST)
        {
            foreach(Room room in roomConnection.Value)
            {
                Vector2Int[] path = AStarPathfinding.RunAStar(grid, roomConnection.Key.Position, room.Position);
                if (path != null)
                {
                    foreach (Vector2Int point in path)
                    {
                         grid.cells[point.x, point.y] = new Cell(CellType.Floor);
                    }
                }
            }
        }


        ToScreen.GridToTilemap(floor, grid, wallTile);
    }
}