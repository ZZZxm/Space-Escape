using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBlue;

    void Start()
    {
        Instantiate(playerBlue, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }
}
