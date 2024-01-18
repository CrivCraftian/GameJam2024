using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ToScreen
{
    public static void PathToTilemap(Tilemap tilemap, Grid grid, Tile tile, Vector2Int[] path)
    {
        foreach(Vector2Int point in path)
        {
            foreach(Vector2 otherPoint in grid.FindCellNeighboursVectorSurround(point))
            {
                if (grid.cells[(int)otherPoint.x, (int)otherPoint.y] != null)
                {
                    continue;
                }
                else
                {
                    grid.cells[(int)otherPoint.x, (int)otherPoint.y] = new Cell(CellType.Wall);
                }
            }
        }
    }

    public static void WallToTilemap(Tilemap tilemap, Grid grid, Tile tileUP)
    {
        for (int i = 0; i < grid.gridX; i++)
        {
            for (int j = 0; j < grid.gridY; j++)
            {
                if (grid.cells[i, j] == null) continue;

                if (grid.cells[i, j].CellType == CellType.Wall)
                {
                    // tilemap.SetTile(new Vector3Int(i, j, 1), tile);
                    Vector2Int[] neighbours = grid.FindCellNeighboursVectorUDLR(new Vector2Int(i, j));

                    foreach(Vector2Int neighbour in neighbours)
                    {
                        if (grid.cells[neighbour.x, neighbour.y]==null)
                        {
                            continue;
                        }

                        Cell currentCell = grid.cells[neighbour.x, neighbour.y];
                        if(currentCell.CellType!=CellType.Floor)
                        {
                            continue;
                        }

                        if (neighbour.x == i && neighbour.y < j)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 1), tileUP);
                            break;
                        }
                    }
                }
            }
        }
    }

    public static void GridToTilemap(Tilemap tilemap, Grid grid, Tile tile)
    {
        for(int i = 0; i < grid.gridX; i++)
        {
            for(int j = 0; j < grid.gridY; j++)
            {
                if (grid.cells[i, j] == null) continue;

                if (grid.cells[i, j].CellType == CellType.Floor)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 1), tile);
                }

            }
        }
    }

    public static void RoomToTilemap(Tilemap tilemap, Grid grid, Tile tile, Room[] rooms)
    {
        foreach(Room room in rooms)
        {
            for(int i = 0;i < room.sizeX+2; i++)
            {
                for(int j = 0;j < room.sizeY+2; j++)
                {
                    if (i == 0 || i == room.sizeX+1 || j == 0 || j == room.sizeY+1)
                    {
                        Vector2 tempPosition = new Vector2Int(room.Position.x - room.sizeX / 2 + i, room.Position.y - room.sizeY / 2 + j);
                        if (grid.cells[(int)tempPosition.x, (int)tempPosition.y] != null)
                        {
                            continue;
                        }
                        else
                        {
                            grid.cells[(int)tempPosition.x, (int)tempPosition.y] = new Cell(CellType.Wall);
                        }
                    }
                    else
                    {
                        grid.cells[room.Position.x - room.sizeX / 2 + i, room.Position.y - room.sizeY / 2 + j] = new Cell(CellType.Floor);
                    }
                }
            }
        }
    }
}
