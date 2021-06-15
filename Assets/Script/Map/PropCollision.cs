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
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    propLayer.SetTile(new Vector3Int(boxCell.x + i, boxCell.y + j, 0), null);
                }
            }
            JourneyManager.getInstance().ChangeBoxNum(-1);
            randomDropProp();
            if (JourneyManager.getInstance().winCase == 1 && JourneyManager.getInstance().boxNum == 0)
            {
                JourneyManager.getInstance().UnitOverToNextLevel();
            }
        }
    }

    private void randomDropProp()
    {
        int ranNum = Random.Range(0, 100);
        string propInfo = "您已开启宝箱";
        if (ranNum < 10)
        {
            // 回血
            JourneyManager.getInstance().ChangeItems(0, 1);
            propInfo = "您已开启宝箱，获得回血道具";
        }
        else if (ranNum < 30)
        {
            // 回蓝
            JourneyManager.getInstance().ChangeItems(1, 1);
            propInfo = "您已开启宝箱，获得增蓝道具";
        }
        else if (ranNum < 40)
        {
            // 加速
            JourneyManager.getInstance().ChangeItems(2, 1);
            propInfo = "您已开启宝箱，获得增敏道具";
        }
        else if (ranNum < 50)
        {
            // 加防御
            JourneyManager.getInstance().ChangeItems(3, 1);
            propInfo = "您已开启宝箱，获得增耐道具";
        }
        else if (ranNum < 60)
        {
            // 帽子
            JourneyManager.getInstance().ChangeClothes(0);
            propInfo = "您已开启宝箱，获得装备帽子";
        }
        else if (ranNum < 70)
        {
            // 护甲
            JourneyManager.getInstance().ChangeClothes(1);
            propInfo = "您已开启宝箱，获得装备护甲";
        }
        else if (ranNum < 80)
        {
            // 鞋子
            JourneyManager.getInstance().ChangeClothes(2);
            propInfo = "您已开启宝箱，获得装备鞋子";
        }
        else if (ranNum < 90)
        {
            // 饰品
            JourneyManager.getInstance().ChangeClothes(3);
            propInfo = "您已开启宝箱，获得装备饰品";
        }
        JourneyManager.getInstance().OpenBox(propInfo);
        Invoke("clearPropInfo", 5);
    }

    private void clearPropInfo()
    {
        JourneyManager.getInstance().OpenBox("");
    }
}
