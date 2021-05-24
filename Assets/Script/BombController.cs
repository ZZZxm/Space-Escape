using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject boomEffect;
    private int range;
    public void Init(int range,int delayTime)
    {
        this.range = range;
        StartCoroutine("DelayBoom", delayTime);
    }
    

    IEnumerator DelayBoom(int time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(boomEffect, transform.position, Quaternion.identity);
        Boom(Vector2.left);
        Boom(Vector2.right);
        Boom(Vector2.up);
        Boom(Vector2.down);
        Destroy(gameObject);
    }

    private void Boom(Vector2 dir)
    {
        for (int i = 1; i <= range; i++)
        {
            GameObject effect = Instantiate(boomEffect);
            Vector2 pos = (Vector2)transform.position + dir * i;
            effect.transform.position = pos;
        }
    }
}
