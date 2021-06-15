using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSide : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;

    public int attack = 250;
    // Start is called before the first frame update
    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("hit enemy!");
            enemy.hurt(JourneyManager.getInstance().atts[2] * 2);
        }
    }
}
