using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : PropCollision
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
            JourneyManager.getInstance().ChangeItems(2, 1);
            Destroy(this.gameObject);
            Debug.Log("加速道具：" + JourneyManager.getInstance().items[2]);
        }    
    }
}
