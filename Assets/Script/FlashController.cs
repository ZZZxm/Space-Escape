using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Animator animator;
    
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector2 fireDirection)
    {
        if (fireDirection.x == -1)
        {
            transform.Translate(-1.5f,0.55f,0);
        }
        else if (fireDirection.x == 1)
        {
            transform.Translate(1.5f,0.55f,0);
            transform.Rotate(new Vector3(0,0,180));
        }
        else if (fireDirection.y == -1)
        {
            transform.Translate(0.05f, -1f, 0);
            transform.Rotate(new Vector3(0, 0, 90));
        } 
        else if (fireDirection.y == 1)
        {
            transform.Translate(-0.05f, 2.5f, 0);
            transform.Rotate(new Vector3(0, 0, -90));
        }

        Destroy(gameObject, 1);
    }
}
