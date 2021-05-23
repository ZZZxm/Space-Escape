using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    // Used to store a reference to the Player's animator component.
    protected Animator animator;
    public LayerMask blockingLayer;// use to detect whether has collision
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb2D;
    public float speed;// make calculation of moveTime more quickly
    Vector2 movement;
    bool death;
    public int totalBlood;
    public int currentBlood;
    public GameObject bulletPrefab;
    protected Vector2 lookDirection = new Vector2(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentBlood <= 0)
        {
            animator.SetTrigger("Death");
        }
        // turn
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("turnFront", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("turnLeft", true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("turnBack", true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("turnRight", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("turnFront", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("turnLeft", false);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("turnBack", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("turnRight", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            normalAttack();
            Debug.Log("normal attack");
        }

        // move
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        lookDirection = movement;
        if (movement == new Vector2(0, 0))
        {
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("run", true);
        }
    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
    }
    public void hurt(int deltaBlood)
    {
        this.currentBlood -= deltaBlood;
        Debug.Log("Player blood left: " + currentBlood);
    }
    public int getCurrentBlood()
    {
        return this.currentBlood;
    }
    public abstract void normalAttack();
}
