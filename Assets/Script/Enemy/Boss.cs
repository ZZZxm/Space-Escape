using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Start is called before the first frame update    
    new void Start()
    {
        base.Start();
        // 怪物属性设置
        this.maxBlood = 5000;
        this.blood = this.maxBlood;
        this.attack = 100;
        this.defend = 20;
        this.viewRadius = 120.0f;
    }

    // Update is called once per frame

    public override void AttackPlayer()
    {
        Vector3 dir = target.position - transform.position;
        float forwardDistance = Vector3.Dot(dir, transform.forward.normalized);
        float rightDistance = Vector3.Dot(dir, transform.right.normalized);
    }

    public override void hurt(int deltaBlood)
    {
        Debug.Log("Boss Blood: "+blood);
        base.hurt(deltaBlood);
    }
}
