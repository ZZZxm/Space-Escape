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
    private int totalMP;
    protected int curMP;
    protected int deltaEMP;
    protected int deltaQMP;
    protected int power;
    protected int patience;
    
    protected Vector2 lookDirection = new Vector2(0, -1);

    protected void Awake()
    {
        totalBlood = JourneyManager.getInstance().playerHPMax;
        currentBlood = JourneyManager.getInstance().playerCurHP;
        JourneyManager.getInstance().playerController = this;
        totalMP = JourneyManager.getInstance().playerMPMax;
        curMP = JourneyManager.getInstance().playerCurMP;
        power = JourneyManager.getInstance().atts[2];
        patience = JourneyManager.getInstance().atts[3];
    }

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
        Debug.Log("currentBlood: "+currentBlood);
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
            animator.SetTrigger("Attack");
            Debug.Log("normal attack");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bDodge = true;
            animator.SetTrigger("Duck");
            StartCoroutine("DelayNoDodge", 0.3);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            qSkill();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (this.curMP - deltaEMP > 0)
            {
                this.curMP -= deltaEMP;
                JourneyManager.getInstance().ChangePlayerMP(-deltaEMP);
                eSkill();
            }
        }
    }

    private void FixedUpdate()
    {
        if (bDodge)
        {
            rb2D.MovePosition(rb2D.position - 2 * lookDirection * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    public void hurt(int deltaBlood)
    {
        deltaBlood -= (int)(patience / 100);
        if (bDodge)
        {
            deltaBlood = Mathf.RoundToInt(deltaBlood * 0.5f);
            Debug.Log("Dodge success!");
        }
        
        this.currentBlood -= deltaBlood;
        JourneyManager.getInstance().ChangePlayerHP(-deltaBlood);
        animator.SetTrigger("Hit");
        //Debug.Log("Player blood left: " + currentBlood);
    }

    public void addHp()
    {
        this.currentBlood = JourneyManager.getInstance().playerCurHP;
    }

    public void hpMax()
    {
        this.totalBlood = JourneyManager.getInstance().playerHPMax;
    }

    public void addMp()
    {
        this.curMP = JourneyManager.getInstance().playerCurMP;
    }

    public void mpMax()
    {
        this.totalMP = JourneyManager.getInstance().playerMPMax;
    }

    public void addPower()
    {
        this.power = JourneyManager.getInstance().atts[2];
    }
    public void addPatience()
    {
        this.patience = JourneyManager.getInstance().atts[3];
    }

    public int getCurrentBlood()
    {
        return this.currentBlood;
    }

    public abstract void normalAttack();

    public abstract void qSkill();

    public abstract void eSkill();

    IEnumerator DelayNoDodge(float time)
    {
        yield return new WaitForSeconds(time);
        bDodge = false;
    }
}