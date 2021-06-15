using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PropCollision
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            JourneyManager.getInstance().ChangeMoney(5);
            Destroy(this.gameObject);
            Debug.Log("Coin: " + JourneyManager.getInstance().money);
        }    
    }
}
