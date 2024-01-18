using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public Cell?[,] cells;

    public int gridX { get; private set; }
    public int gridY { get; private set; }

    public Grid(int x, int y)
    {
        this.gridX = x;
        this.gridY = y;

        cells = GridGenerator.GenerateGrid(x, y);
    }

    public Vector2Int[] FindCellNeighboursVector(Vector2Int position)
    {
        List<Vector2Int> neigbhours = new List<Vector2Int>();

        if(position.x > 0)
        {
            neigbhours.Add(new Vector2Int(position.x-1, position.y));
        }
        if (position.x < gridX-1)
        {
            neigbhours.Add(new Vector2Int(position.x+1, position.y));
        }
        if (position.y > 0)
        {
            neigbhours.Add(new Vector2Int(position.x, position.y - 1));
        }
        if (position.y < gridY-1)
        {
            neigbhours.Add(new Vector2Int(position.x, position.y + 1));
        }

        return neigbhours.ToArray();
    }
}
