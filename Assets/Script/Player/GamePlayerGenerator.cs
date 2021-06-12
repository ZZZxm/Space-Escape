using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBlue;
    public bool astro = true;

    // Start is called before the first frame update
    void Start()
    {
        if (astro)
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
