using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameObject journeyManager;
   
    void Awake()
    {
        if(JourneyManager.instance==null)
        {
            Instantiate(journeyManager);
        }
    }
}
