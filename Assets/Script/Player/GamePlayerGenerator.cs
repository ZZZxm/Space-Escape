using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBlue;
    public GameObject PlayerSword;
    private int playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = JourneyManager.getInstance().playerInfo;
        if (playerInfo % 3 == 1)
        {
            Instantiate(player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
        else if (playerInfo % 3 == 0)
        {
            Instantiate(playerBlue, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
        else if (playerInfo % 3 == 2)
        {
            Instantiate(PlayerSword, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}