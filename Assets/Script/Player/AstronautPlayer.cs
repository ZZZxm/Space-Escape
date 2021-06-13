using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautPlayer : PlayerController
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public GameObject bulletLargePrefab;
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public override void normalAttack()
    {
        Debug.Log("Astronaut normal attack!!");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletController bc = bullet.transform.GetComponent<BulletController>();
        if (bc != null)
        {
            Debug.Log(lookDirection);
            bc.Move(lookDirection, 2000);
        }
        //throw new System.NotImplementedException();
    }

    public override void qSkill()
    {
        GameObject largeBullet = Instantiate(bulletLargePrefab, transform.position, Quaternion.identity);
        BulletLargeController bc = largeBullet.transform.GetComponent<BulletLargeController>();
        if (bc != null)
        {
            Debug.Log(lookDirection);
            bc.Move(lookDirection, 500);
        }
    }

    public override void eSkill()
    {
        GameObject[] bullets = new GameObject[13];
        BulletController bc;
        int i = 1;
        float c, s;
        Vector2 dir = new Vector2();

        while (i <= 6)
        {
            bullets[11-i] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bc = bullets[11-i].transform.GetComponent<BulletController>();
            c = Mathf.Cos((float)0.25 * i);
            s = Mathf.Sin((float)0.25 * i);
            dir.x = c * lookDirection.x - s * lookDirection.y;
            dir.y = s * lookDirection.x + c * lookDirection.y;
            if (bc != null)
            {
                Debug.Log(dir);
                bc.Move(dir, 3000);
            }
            dir.x = c * lookDirection.x + s * lookDirection.y;
            dir.y = c * lookDirection.y - s * lookDirection.x;
            bullets[i] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bc = bullets[i++].transform.GetComponent<BulletController>();
            if (bc != null)
            {
                Debug.Log(dir);
                bc.Move(dir, 3000);
            }
        }

        bullets[12] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bc = bullets[12].transform.GetComponent<BulletController>();
        bc.Move(lookDirection, 3000);

    }
}
