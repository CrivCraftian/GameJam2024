using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Grid grid = new Grid(dungeonSizeX, dungeonSizeY);



        ToScreen.GridToTilemap(floor, grid, wallTile);
    }
}
