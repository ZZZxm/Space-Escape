using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLargeController : BulletController
{
    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        Debug.Log("large bullet hit enemy!");
    //        enemy.hurt(enemy.maxBlood);
    //    }
    //    Destroy(this.gameObject);
    //}
    private void OnTriggerEnter2D (Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("large bullet hit enemy!");
            enemy.hurt(enemy.maxBlood);
        }
    }
}
