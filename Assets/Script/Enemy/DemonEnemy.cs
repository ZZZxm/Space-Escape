using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonEnemy : Enemy
{
    private float attackRange = 2.0f;
    private float attackWidth = 1.0f;


    private new void Update()
    {
        base.Update();
        //Debug.Log("1111111111");
    }
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
            Debug.Log("Demon hurt the player with " + this.attack + " points!");
        }   
    }

    public override void hurt(int deltaBlood)
    {
        blood -= deltaBlood;
        Debug.Log("Enemy Blood: "+blood);

        Canvas hpBar = GetComponentInChildren<Canvas>();
        Slider slider = hpBar.GetComponentInChildren<Slider>();
        slider.value = (float)blood / (float)maxBlood;


        if (blood <= 0)
        {
            this.rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("Die", true);
            Destroy(this.gameObject, 3.0f);
        }
    }
}
