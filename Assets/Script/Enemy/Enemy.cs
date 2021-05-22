using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int attack;
    public int defend;
    private Animator animator;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        Debug.Log(dir);
        if (dir.x > 0) {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
        if (dir.x < 0) {
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }

        float dis = dir.sqrMagnitude;
        if (dis < 2.0f) {
            animator.SetBool("Attack", true);
        } else {
            animator.SetBool("Attack", false);
        }
    }
}
