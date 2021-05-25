using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public int attack;
    public int defend;
    protected Animator animator;
    protected Transform target;
    protected Rigidbody2D rb2D;
    public int currentBlood;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        // Debug.Log(dir);
        if (dir.x > 0) {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
        if (dir.x < 0) {
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }

        float dis = dir.sqrMagnitude;
        if (dis < 2.0f) {
            animator.SetBool("Attack", true);
            // Debug.Log("You attack the player!");
        } else {
            animator.SetBool("Attack", false);
        }
    }

    public abstract void AttackPlayer();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombEffect"))
        {
            hurt(GameObject.FindGameObjectWithTag("BombEffect").GetComponent<ExplodeController>().attack);
        }
    }

    public void hurt(int deltaBlood)
    {
        currentBlood -= deltaBlood;
        Debug.Log("Enemy Blood:"+currentBlood);
    }
}
