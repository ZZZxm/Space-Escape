using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlue : PlayerController
{
    public GameObject bombPrefab;

    private Boolean cool=false;

    private Boolean consecutive = false;
    // Start is called before the first frame update
    new private void Awake()
    {
        base.Awake();
        this.deltaEMP = 3;
        this.deltaQMP = 5;
    }
    void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        cool = false;
    }

    new protected void Update()
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
        if (cool==false|| this.consecutive)
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
        Attack(1,consecutive);
    }

    public override void qSkill()
    {
        if (this.curMP - deltaQMP >= 0)
        {
            this.curMP -= deltaQMP;
            JourneyManager.getInstance().ChangePlayerMP(-deltaQMP);
            Attack(3);
        }
    }

    public override void eSkill()
    {
        if (this.curMP - deltaEMP >= 0)
        {
            this.curMP -= deltaEMP;
            JourneyManager.getInstance().ChangePlayerMP(-deltaEMP);
            consecutive = true;
            StartCoroutine("UnsetCon", 2);
        }
    }
    
    IEnumerator CanAttack(int time)
    {
        yield return new WaitForSeconds(time);
        cool = false;
    }
    
    IEnumerator UnsetCon(int time)
    {
        yield return new WaitForSeconds(time);
        consecutive = false;
    }
    
    
}
