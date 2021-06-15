using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapGenerator : MonoBehaviour
{

    public GameObject mapPrefab;

    public int roomNumber;

    public float xOffset;
    public float yOffset;

    public int totalBoxNumber;

    private List<GameObject> roomList = new List<GameObject>();

    private int[] dirx = { 1, 0, -1, 0 };
    private int[] diry = { 0, 1, 0, -1 };

    float[,] roomPos;

    // Start is called before the first frame update
    void Start()
    {
        roomNumber = JourneyManager.getInstance().roomNumber;
        totalBoxNumber = JourneyManager.getInstance().boxNum;

        int[] roomBoxArr = new int[roomNumber];
        for (int i = 0; i < roomNumber; i++)
        {
            roomBoxArr[i] = 0;
        }
        for (int i = 0; i < totalBoxNumber; i++)
        {
            roomBoxArr[Random.Range(0, roomNumber)]++;
        }

        roomPos = new float[roomNumber, 2];
        int[,] hasPath = new int[roomNumber, 4];

        roomPos[0, 0] = 0.0f;
        roomPos[0, 1] = 0.0f;

        for (int i = 0; i < roomNumber; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(j);
                hasPath[i, j] = 0;
            }
        }

        for (int i = 1; i < roomNumber; i++)
        {
            int curDir = Random.Range(0, 4);
            float newx = roomPos[i - 1, 0] + dirx[curDir] * xOffset;
            float newy = roomPos[i - 1, 1] + diry[curDir] * yOffset;
            if (checkRoomExist(i, newx, newy) >= 0)
            {
                i -= 1;
                continue;
            }
            roomPos[i, 0] = newx;
            roomPos[i, 1] = newy;
        }

        for (int i = 0; i < roomNumber; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float newx = roomPos[i, 0] + dirx[j] * xOffset;
                float newy = roomPos[i, 1] + diry[j] * yOffset;
                if (checkRoomExist(roomNumber, newx, newy) >= 0)
                {
                    hasPath[i, j] = 1;
                }
            }
        }

        for (int i = 0; i < roomNumber; i++)
        {
            GameObject newRoom = Instantiate(mapPrefab, new Vector3(roomPos[i, 0], roomPos[i, 1], 0.0f), Quaternion.identity);
            newRoom.GetComponent<MapController>().boxNumber = roomBoxArr[i];
            roomList.Add(newRoom);
        }

        for (int i = 0; i < roomNumber; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                roomList[i].GetComponent<MapController>().hasPath[j] = hasPath[i, j];
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            JourneyManager.getInstance().reloadScene("GameStart");
        }
        */
    }

    private int checkRoomExist(int num, float xPos, float yPos)
    {
        for (int i = 0; i < num; i++)
        {
            if (roomPos[i,0] == xPos && roomPos[i,1] == yPos)
            {
                return i;
            }
        }
        return -1;
    }

}
