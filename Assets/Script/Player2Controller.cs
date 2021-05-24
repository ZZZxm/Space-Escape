using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    // Used to store a reference to the Player's animator component.
    protected Animator animator;
    public LayerMask blockingLayer;// use to detect whether has collision
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb2D;
    public float speed;// make calculation of moveTime more quickly
    protected Vector2 movement;
    bool death;
    Vector2 lookDirection = new Vector2(1,0);
    public GameObject flashPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
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
        float faceDirection = Input.GetAxisRaw("Horizontal");
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(-faceDirection, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Rifle");
            GameObject flash = Instantiate(flashPrefab, rb2D.position, Quaternion.identity);
            FlashController fc = flash.GetComponent<FlashController>();
            fc.Fire(lookDirection);
        }
    }
    
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
    }
    

}
