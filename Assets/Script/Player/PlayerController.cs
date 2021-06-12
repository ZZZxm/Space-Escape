using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    // Used to store a reference to the Player's animator component.
    protected Animator animator;
    public LayerMask blockingLayer; // use to detect whether has collision
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb2D;
    public float speed; // make calculation of moveTime more quickly
    Vector2 movement;
    bool death;
    public int totalBlood;
    public int currentBlood;
    private bool bDodge;
    public GameObject bulletPrefab;
    protected Vector2 lookDirection = new Vector2(0, -1);

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
        if (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        {
            if (Mathf.RoundToInt(movement.x) != 0 || Mathf.RoundToInt(movement.y) != 0)
            {
                if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
                {
                    lookDirection.Set(Mathf.RoundToInt(movement.x), 0);
                }
                else
                {
                    lookDirection.Set(0, Mathf.RoundToInt(movement.y));
                }
            }
        }

        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);
        animator.SetFloat("Speed", movement.magnitude);
        if (Input.GetMouseButtonDown(1))
        {
            normalAttack();
            Debug.Log("normal attack");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bDodge = true;
            animator.SetTrigger("Duck");
            StartCoroutine("DelayNoDodge", 0.3);
        }
    }

    private void FixedUpdate()
    {
        if (bDodge)
        {
            rb2D.MovePosition(rb2D.position - 2 * lookDirection * speed * Time.fixedDeltaTime);
            Debug.Log("DODGE");
        }
        else
        {
            rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
            Debug.Log("NO!!");
        }
    }

    public void hurt(int deltaBlood)
    {
        if (bDodge)
        {
            deltaBlood = Mathf.RoundToInt(deltaBlood * 0.5f);
            Debug.Log("Dodge success!");
        }
        
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
    public abstract void dodge();

    IEnumerator DelayNoDodge(float time)
    {
        yield return new WaitForSeconds(time);
        bDodge = false;
    }
}