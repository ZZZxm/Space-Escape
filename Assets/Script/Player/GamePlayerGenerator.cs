using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBlue;
    private int playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo=JourneyManager.getInstance().playerInfo;
        if (playerInfo%2==1)
        {
            Instantiate(player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
        else
        {
            Instantiate(playerBlue, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
