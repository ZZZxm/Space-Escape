using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public enum EnemyState 
{
    Walk, // 巡逻状态
    Trace, // 追踪状态
    Die // 死亡状态
}

public abstract class Enemy : MonoBehaviour
{

    [Header("敌人属性")]
    public int attack; // 攻击力
    public int defend; // 防御力
    public int blood; // 当前血量
    public int maxBlood; // 最大血量
    public float viewRadius; // 怪物视野范围（以自身为中心的一个圆）
    protected Animator animator;
    protected Transform target;
    protected Rigidbody2D rb2D;
    private AIDestinationSetter AIPath;
    public Slider slider;
    protected EnemyState state;
    protected EnemyGenerator enemyGenerator;

    private GameObject coin;
    private GameObject hpItem;
    private GameObject mpItem;
    private GameObject speedItem;
    private GameObject defendItem;



    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        enemyGenerator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyGenerator>();
        state = EnemyState.Walk;
        // 设置AI寻路目标
        target = GameObject.FindGameObjectWithTag("Player").transform;
        AIPath = GetComponent<AIDestinationSetter>();
        AIPath.target = target;
        AIPath.enabled = false;
        // 绑定血条
        Canvas hpBar = GetComponentInChildren<Canvas>();
        slider = hpBar.GetComponentInChildren<Slider>();
        // 获取金币及道具Prefabs
        coin = (GameObject)Resources.Load("Prefabs/Item/coin");
        hpItem = (GameObject)Resources.Load("Prefabs/Item/HPItem");
        mpItem = (GameObject)Resources.Load("Prefabs/Item/MPItem");
        speedItem = (GameObject)Resources.Load("Prefabs/Item/speedItem");
        defendItem = (GameObject)Resources.Load("Prefabs/Item/defendItem");
    }

    protected void Update()
    {
        if (blood <= 0)
        {
            return;
        }

        // 动画控制
        Vector3 dir = target.position - transform.position;
        // Vector2 velocity = rb2D.velocity;
        // Debug.Log(velocity);
        animator.SetFloat("Vertical", dir.y);
        animator.SetFloat("Horizonal", dir.x);

        float dis = dir.sqrMagnitude; // 与玩家的距离

        if (dis < viewRadius)
        {
            AIPath.enabled = true;
            animator.SetBool("Run", true);
        }
        else
        {
            AIPath.enabled = false;
            animator.SetBool("Run", false);
        }

        if (dis < 2.0f)
        {
            animator.SetBool("Attack", true);
            // Debug.Log("You attack the player!");
        }
        else
        {
            animator.SetBool("Attack", false);
        }

    }

    // 攻击玩家，不同敌人的攻击方式不同
    public abstract void AttackPlayer();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombEffect"))
        {
            hurt(GameObject.FindGameObjectWithTag("BombEffect").GetComponent<ExplodeController>().attack);
        }
    }

    // 敌人受伤
    public virtual void hurt(int deltaBlood)
    {   
        animator.SetBool("Hit", true);

        blood -= deltaBlood;
        slider.value = (float)blood / (float)maxBlood;

        if (blood <= 0 && !(this.state == EnemyState.Die))
        {
            Die();
        }
        else
        {
           // Invoke("DelayFromHurt", 0.371f);
           animator.SetBool("Hit", false);
        }
    }

    private void DelayFromHurt()
    {
        animator.SetBool("Hit", false);
    }

    private void Die()
    {   
        Vector3 diePos = transform.position;
 
        this.state = EnemyState.Die;
        this.rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("Death", true);
        Destroy(this.gameObject, 2.0f);
        DropProps(diePos);
        this.enemyGenerator.NumOfSmallEnemies--;
        Debug.Log("Enemy Left: " + this.enemyGenerator.NumOfSmallEnemies);
    }

    private void DropProps(Vector3 pos)
    {
        int seed = Random.Range(0, 10);
        if (seed < 7)
        {
            Instantiate(coin, pos, Quaternion.identity);// 掉落金币
        }
        else
        {   
            int p = Random.Range(0, 4);
            switch(p)
            {
                case 0:
                {
                    Instantiate(hpItem, pos, Quaternion.identity);
                    break;
                }
                case 1:
                {
                    Instantiate(mpItem, pos, Quaternion.identity);
                    break;
                }
                case 2:
                {
                    Instantiate(speedItem, pos, Quaternion.identity);
                    break;
                }
                case 3:
                {
                    Instantiate(defendItem, pos, Quaternion.identity);
                    break;
                }
            }
        }
        
    }
}
