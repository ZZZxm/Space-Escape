using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmallEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        Debug.Log("111111");
    }

    public override abstract void AttackPlayer();

    public override abstract void hurt(int deltaBlood);
}
