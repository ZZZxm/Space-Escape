using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlue : PlayerController
{
    public GameObject bombPrefab;

    private Boolean cool=false;
    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        cool = false;
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

    private void Attack(int range,Boolean consecutive=false)
    {
        if (cool==false)
        {
            if (!consecutive)
            {
                cool = true;
                StartCoroutine("CanAttack", 2);
            }
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x + 0.5f) - 0.5f,
                Mathf.RoundToInt(transform.position.y));
            bomb.GetComponent<BombController>().Init(1, range);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombEffect"))
        {
            hurt(GameObject.FindGameObjectWithTag("BombEffect").GetComponent<ExplodeController>().attack);
        }
    }

    public override void normalAttack()
    {
        Attack(1);
    }

    public override void qSkill()
    {
        Attack(3);
    }

    public override void eSkill()
    {
        Attack(1,true);
    }
    
    IEnumerator CanAttack(int time)
    {
        yield return new WaitForSeconds(time);
        cool = false;
    }
}
