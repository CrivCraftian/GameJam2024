using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ToScreen
{
    public static void GridToTilemap(Tilemap tilemap, Grid grid, Tile tile)
    {
        for(int i = 0; i < grid.gridX; i++)
        {
            for(int j = 0; j < grid.gridY; j++)
            {
                if (grid.cells[i, j] == null) continue;

                if (grid.cells[i, j].CellType == CellType.Wall)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 1), tile);
                }
            }
        }
    }
}
