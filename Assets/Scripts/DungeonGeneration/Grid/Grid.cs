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

        cells = new Cell[gridX, gridY];
    }

    public void FindCellNeighbours()
    {

    }
}
