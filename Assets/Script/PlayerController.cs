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
    protected Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (currentBlood <= 0)
        {
            animator.SetTrigger("Death");
        }
        // turn
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if(!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        {
            lookDirection.Set(movement.x, movement.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);
        animator.SetFloat("Speed", movement.magnitude);
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
        {
            normalAttack();
            Debug.Log("normal attack");
        }
    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
    }
    public void hurt(int deltaBlood)
    {
        this.currentBlood -= deltaBlood;
        JourneyManager.getInstance().ChangePlayerHP(-deltaBlood);
        animator.SetTrigger("Hit");
        Debug.Log("Player blood left: " + currentBlood);
    }
    public int getCurrentBlood()
    {
        return this.currentBlood;
    }
    public abstract void normalAttack();
}
