using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //游戏人物 的位置
    private Transform playerPosition;

    //游戏人物与 相机的差
    private Vector3 offset;

    //相机的速度
    private float smoothing = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = playerPosition.position + playerPosition.TransformDirection(offset);
        //Vector3.Lerp 计算相机位置 和 目标位置的插值
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        //相机的目标看向 游戏人物
        transform.LookAt(playerPosition.position);

        if (transform.localEulerAngles.z != 0)
        {
            float rotX = transform.localEulerAngles.x;
            float rotY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
    }
}
