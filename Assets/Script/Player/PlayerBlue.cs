using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlue : PlayerController
{
    public GameObject bombPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        base.Update();
        float faceDirection = Input.GetAxisRaw("Horizontal");
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(-faceDirection, 1, 1);
        }
    }
    

    public override void normalAttack()
    {
        GameObject bomb=Instantiate(bombPrefab);
        bomb.transform.position=new Vector3(Mathf.RoundToInt(transform.position.x+0.5f)-0.5f,Mathf.RoundToInt(transform.position.y));
        bomb.GetComponent<BombController>().Init(2,1);
    }

    public override void qSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void eSkill()
    {
        throw new System.NotImplementedException();
    }
}
