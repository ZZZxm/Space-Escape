using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private Transform playerPosition;

    //游戏人物与相机的差
    private Vector3 offset;

    private float cameraSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        getPlayerInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            getPlayerInfo();
        }

        if (player == null)
        {
            return;
        }

        Vector3 targetPosition = playerPosition.position + playerPosition.TransformDirection(offset);

        /*
        if (playerPosition.position.x < -10.0f)
        {
            targetPosition.x = transform.position.x;
        }

        if (playerPosition.position.y <= -13.0f || playerPosition.position.y >= 14.0f)
        {
            targetPosition.y = transform.position.y;
        }
        */

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);

    }

    private void getPlayerInfo()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            offset = transform.position - playerPosition.position;
        }
    }

}
