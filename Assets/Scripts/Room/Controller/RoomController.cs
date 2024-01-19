using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    public Room[] roomSet {  private get; set; }
    [Header("Player")]
    [SerializeField] GameObject playerObject;

    [Header("Room Objects")]
    [SerializeField] List<GameObject> roomObjects = new List<GameObject>();

    [Header("Enemies")]
    [SerializeField] GameObject smallEnemy;
    [SerializeField] GameObject mediumEnemy;
    [SerializeField] GameObject largeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = SpawnPlayer(playerObject);

        foreach (Room room in roomSet)
        {
            SpawnObjects(roomObjects, room);
            SpawnEnemies(smallEnemy, mediumEnemy, largeEnemy, room, player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject SpawnPlayer(GameObject player)
    {
        int randRoom = Random.Range(0, roomSet.Length - 1);

        Vector2Int spawnPos = new Vector2Int(roomSet[randRoom].Position.x + roomSet[randRoom].sizeX/2, roomSet[randRoom].Position.y + roomSet[randRoom].sizeY/2);

        GameObject tmpPlayer = Instantiate(playerObject, (Vector2)spawnPos, Quaternion.identity);

        return tmpPlayer;
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

    public void SpawnEnemies(GameObject smallEnemy, GameObject mediumEnemy, GameObject largeEnemy, Room room, GameObject player)
    {
        for (int i = 0; i < room.sizeX; i++)
        {
            for (int j = 0; j < room.sizeY; j++)
            {
                    Vector2 tempPosition = new Vector2Int(room.Position.x - room.sizeX / 2 + i, room.Position.y - room.sizeY / 2 + j);
                    int randNum = Random.Range(0, 200);

                    if(randNum == 0)
                    {
                        GameObject newEnemy = Instantiate(smallEnemy, tempPosition, Quaternion.identity, this.transform);
                    newEnemy.GetComponent<Enemy>().player = player;
                    }
                    else if(randNum > 5 && randNum < 10)
                    {
                    GameObject newEnemy = Instantiate(mediumEnemy, tempPosition, Quaternion.identity, this.transform);
                    newEnemy.GetComponent<Enemy>().player = player;
                }
                    else if(randNum > 10 && randNum < 20)
                    {
                    GameObject newEnemy = Instantiate(largeEnemy, tempPosition, Quaternion.identity, this.transform);
                    newEnemy.GetComponent<Enemy>().player = player;
                }
            }
        }
    }
}
