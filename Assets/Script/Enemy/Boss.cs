using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float attackRange = 12.0f;

    public float attackWidth = 20.0f;
    // Start is called before the first frame update    
    new void Start()
    {
        base.Start();
        // 怪物属性设置
        int bonus = JourneyManager.getInstance().winNum;

        this.maxBlood = 5000 + bonus * 500;
        this.blood = this.maxBlood;
        this.attack = 100 + bonus * 10;
        this.defend = 10 + bonus * 10;
        this.viewRadius = 150.0f;
    }

    new void Update()
    {
        base.Update();
        Vector3 dir = target.position - transform.position;
        float dis = dir.sqrMagnitude;
        Debug.Log("Boss Update");

        if (dis < 20.0f)
        {
            animator.SetBool("Attack", true);
            // Debug.Log("You attack the player!");
        }
        else
        {
            animator.SetBool("Attack", false);
        }
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
        if (blood <= 0)
        {
            enemyGenerator.winJourney = true;
        }
    }

    override protected void DropProps(Vector3 pos)
    {
        float randX = Random.Range(-15.0f, 15.0f);
        float randY = Random.Range(-15.0f, 15.0f);

        int coinNum = 15;
        for (int i = 0; i < coinNum; i++)
        {
            Instantiate(coin, new Vector3(pos.x + randX, pos.y + randY, 0), Quaternion.identity);
        }
    }
}
