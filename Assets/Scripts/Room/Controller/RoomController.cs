using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    public Room[] roomSet {  private get; set; }
    [SerializeField] List<GameObject> roomObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Room room in roomSet)
        {
            SpawnObjects(roomObjects, room);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObjects(List<GameObject> objects, Room room)
    {
            for (int i = 0; i < room.sizeX; i++)
            {
                for (int j = 0; j < room.sizeY; j++)
                {
                    if (i == 1 || i == room.sizeX-1 || j == 1 || j == room.sizeY-1)
                    {
                        Vector2 tempPosition = new Vector2Int(room.Position.x - room.sizeX / 2 + i, room.Position.y - room.sizeY / 2 + j);
                        int randNum = Random.Range(0, 30);

                        switch (randNum)
                        {
                            case 0:
                                int rndNum2 = Random.Range(0, objects.Count-1);
                                Instantiate(objects[rndNum2], tempPosition, Quaternion.identity, this.transform);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
    }
}
