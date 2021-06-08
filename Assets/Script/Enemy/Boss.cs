using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float attackRange = 6.0f;

    public float attackWidth = 4.0f;
    // Start is called before the first frame update    
    new void Start()
    {
        base.Start();
        // 怪物属性设置
        this.maxBlood = 5000;
        this.blood = this.maxBlood;
        this.attack = 100;
        this.defend = 100;
        this.viewRadius = 70.0f;
    }

    // Update is called once per frame

    public override void AttackPlayer()
    {
        Vector3 dir = target.position - transform.position;
        float forwardDistance = Vector3.Dot(dir, transform.forward.normalized);
        float rightDistance = Vector3.Dot(dir, transform.right.normalized);

        if (Mathf.Abs(forwardDistance) <= attackRange && Mathf.Abs(rightDistance) <= attackWidth) 
        {
            // 在攻击范围内
            PlayerController player = target.GetComponent<PlayerController>();
            player.hurt(this.attack);
            Debug.Log("The Boss hurt the player with " + this.attack + " points!");
        }  
    }

    public override void hurt(int deltaBlood)
    {
        Debug.Log("Boss Blood: "+blood);
        base.hurt(deltaBlood);
    }
}
