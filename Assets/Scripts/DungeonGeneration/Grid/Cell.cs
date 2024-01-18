using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Wall,
    Floor
}

public class Cell
{
    public CellType CellType {  get; set; }

    public Cell(CellType cellType)
    {
        this.CellType = cellType;
    }
}
