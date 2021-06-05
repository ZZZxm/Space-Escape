using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMonster : SmallEnemy
{
    
    
    new void Start()
    {
        base.Start();
        // 怪物属性设置
        this.maxBlood = 500;
        this.blood = this.maxBlood;
        this.attack = 40;
        this.defend = 30;
        this.viewRadius = 50.0f;
    }

    // Update is called once per frame

    public override void AttackPlayer()
    {
        Vector2 dir = target.position - transform.position;
        float sqrDis = dir.sqrMagnitude;
    }
}