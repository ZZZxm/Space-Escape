using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
    Flying Monster: 世界1 小怪  
    攻击方式：向前方一格发动攻击
    特点：攻击范围小，攻击力强
*/
public class FlyingMonster : SmallEnemy
{

    private float attackRange = 2.0f;// 攻击范围长度
    private float attackWidth = 1.0f;// 攻击范围宽度


    // Update is called once per frame
    private new void Update()
    {
        base.Update();
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
            Debug.Log("Flying Monster hurt the player with " + this.attack + " points!");
        }   
    }

    public override void hurt(int deltaBlood)
    {
        blood -= deltaBlood;
        Debug.Log("Flying Monster Blood: "+blood);

        slider.value = (float)blood / (float)maxBlood;

        if (blood <= 0)
        {
            this.rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("Die", true);
            Destroy(this.gameObject, 2.5f);
        }
    }

}
