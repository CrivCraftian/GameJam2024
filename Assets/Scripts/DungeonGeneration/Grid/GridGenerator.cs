using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridGenerator
{
    public static Cell[,] GenerateGrid(int gridX, int gridY)
    {
        Cell[,] cells = new Cell[gridX, gridY];

        for (int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                cells[i, j] = null;
            }
        }
        
        return cells;
    }
}
