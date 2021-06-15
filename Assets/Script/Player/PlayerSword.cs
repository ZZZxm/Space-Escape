using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : PlayerController
{
    // Start is called before the first frame update
    private PolygonCollider2D downCollider;
    private PolygonCollider2D upCollider;
    private PolygonCollider2D sideCollider;
    private int attackDown;
    private int attackUp;
    private int attackSide;

    void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        downCollider = GameObject.Find("AttackDown").GetComponent<PolygonCollider2D>();
        upCollider = GameObject.Find("AttackUp").GetComponent<PolygonCollider2D>();
        sideCollider = GameObject.Find("AttackSide").GetComponent<PolygonCollider2D>();
        attackDown = GameObject.Find("AttackDown").GetComponent<PlayerAttackDown>().attack;
        attackUp = GameObject.Find("AttackUp").GetComponent<PlayerAttackUp>().attack;
        attackSide = GameObject.Find("AttackSide").GetComponent<PlayerAttackSide>().attack;
    }

    // Update is called once per frame
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
    }

    public override void qSkill()
    {
        attackDown *= 2;
        attackUp *= 2;
        attackSide *= 2;
        StartCoroutine(DisableQSkill());
    }

    IEnumerator DisableQSkill()
    {
        yield return new WaitForSeconds(2);
        attackDown /= 2;
        attackUp /= 2;
        attackSide /= 2;
    }

    public override void eSkill()
    {
        speed *= 2;
        StartCoroutine(DisableESkill());
    }
    
    IEnumerator DisableESkill()
    {
        yield return new WaitForSeconds(1);
        speed /= 2;
    }

    public void AttackDown()
    {
        downCollider.enabled = true;
        StartCoroutine(DisableAttack());
    }

    public void AttackUp()
    {
        upCollider.enabled = true;
        StartCoroutine(DisableAttack());
    }

    public void AttackSide()
    {
        sideCollider.enabled = true;
        StartCoroutine(DisableAttack());
    }

    IEnumerator DisableAttack()
    {
        yield return new WaitForSeconds(1);
        downCollider.enabled = false;
        upCollider.enabled = false;
        sideCollider.enabled = false;
    }
}