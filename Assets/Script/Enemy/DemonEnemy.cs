using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemy : Enemy
{
    private float attackRange = 2.0f;
    private float attackWidth = 1.0f;
    public override void AttackPlayer()
    {
        Vector3 dir = target.position - transform.position;
        float forwardDistance = Vector3.Dot(dir, transform.forward.normalized);
        float rightDistance = Vector3.Dot(dir, transform.right.normalized);

        if (Mathf.Abs(forwardDistance) <= attackRange && Mathf.Abs(rightDistance) <= attackWidth) 
        {
            // 在攻击范围内
            AstronautPlayer player = target.GetComponent<AstronautPlayer>();
            player.hurt(this.attack);
            Debug.Log("Demon hurt the player with " + this.attack + " points!");
        }   
    }


}
