using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautPlayer : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public override void normalAttack()
    {
        Debug.Log("normal attack!!");
        //throw new System.NotImplementedException();
    }
}
