using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmallEnemy : Enemy
{
    // Start is called before the first frame update

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        // Debug.Log("111111");
    }

    public override abstract void AttackPlayer();

    public override void hurt(int deltaBlood)
    {
        base.hurt(deltaBlood);
    }
}
