using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapGenerator : MonoBehaviour
{

    public GameObject mapPrefab;

    public int roomNumber;

    public float xOffset;

    public int totalBoxNumber;

    private List<GameObject> roomList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int[] roomBoxArr = new int[roomNumber];
        for (int i = 0; i < roomNumber; i++)
        {
            roomBoxArr[i] = 0;
        }
        for (int i = 0; i < totalBoxNumber; i++)
        {
            roomBoxArr[Random.Range(0, roomNumber)]++;
        }

        for (int i = 0; i < roomNumber; i++)
        {
            // roomList.Add(Instantiate(mapPrefab, new Vector3(i * xOffset, 0.0f, 0.0f), Quaternion.identity));
            GameObject newRoom = Instantiate(mapPrefab, new Vector3(i * xOffset, 0.0f, 0.0f), Quaternion.identity);
            newRoom.GetComponent<MapController>().boxNumber = roomBoxArr[i];
            roomList.Add(newRoom);
        }

        roomList[0].GetComponent<MapController>().hasLeftEdge = true;
        roomList[roomNumber - 1].GetComponent<MapController>().hasRightEdge = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
