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


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        state = EnemyState.Walk;
        // 设置AI寻路目标
        target = GameObject.FindGameObjectWithTag("Player").transform;
        AIPath = GetComponent<AIDestinationSetter>();
        AIPath.target = target;
        AIPath.enabled = false;
        // 绑定血条
        Canvas hpBar = GetComponentInChildren<Canvas>();
        slider = hpBar.GetComponentInChildren<Slider>();
    }

    protected void Update()
    {
        // 动画控制
        Vector3 dir = target.position - transform.position;
        if (dir.x > 0)
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
        if (dir.x < 0)
        {
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }

        float dis = dir.sqrMagnitude; // 与玩家的距离

        if (dis < viewRadius)
        {
            AIPath.enabled = true;
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
    public abstract void hurt(int deltaBlood);

}
