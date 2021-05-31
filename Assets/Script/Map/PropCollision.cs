using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PropCollision : MonoBehaviour
{

    private Tilemap propLayer;

    // Start is called before the first frame update
    void Start()
    {
        propLayer = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 collisionPos = collision.bounds.ClosestPoint(collision.transform.position);
            Vector3Int boxCell = propLayer.WorldToCell(collisionPos);
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    propLayer.SetTile(new Vector3Int(boxCell.x + i, boxCell.y + j, 0), null);
                }
            }
            JourneyManager.getInstance().ChangeBoxNum(-1);
        }
    }
}
