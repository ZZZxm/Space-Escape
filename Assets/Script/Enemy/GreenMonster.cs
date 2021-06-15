using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMonster : Enemy
{
    public float attackRadius = 4.0f;
    
    new void Start()
    {
        base.Start();
        // 怪物属性设置
        int bonus = JourneyManager.getInstance().winNum;

        this.maxBlood = 500 + bonus * 40;
        this.blood = this.maxBlood;
        this.attack = 40 + bonus * 7;
        this.defend = 30 + bonus * 3;
        this.viewRadius = 50.0f;
    }

    // Update is called once per frame

    public override void AttackPlayer()
    {
        Vector2 dir = target.position - transform.position;
        float sqrDis = dir.sqrMagnitude;
        if (sqrDis <= attackRadius)
        {
            PlayerController player = target.GetComponent<PlayerController>();
            player.hurt(this.attack);
            Debug.Log("Green Monster hurt the player with " + this.attack + " pts.");
        }
    }
}