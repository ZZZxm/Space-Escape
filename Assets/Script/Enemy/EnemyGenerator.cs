using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameMode 
{
    BeatAll, // 击败所有敌人
    Survival, // 存活一定时间
    TreasureAll, // 获得所有宝箱
    Boss // Boss战
}

/**
    敌人生成器
*/
public class EnemyGenerator : MonoBehaviour
{    
    [Header("游戏模式")]
    public GameMode gameMode;

    public int NumOfSmallEnemies;

    private GameObject player;

    private float intervalTime = 3.0f; // 生成怪物的时间间隔

    static private int MAX_ENEMIES = 10; // 最大怪物数

    private GameObject flyingMonster;

    private GameObject greenMonster;


    public void Start()  //实例化，参数设置为坐标
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NumOfSmallEnemies = 0;
        // 获取所有敌人资源
        flyingMonster = (GameObject)Resources.Load("Prefabs/Enemy/Flying Monster");
        greenMonster = (GameObject)Resources.Load("Prefabs/Enemy/Green Monster");

        // 根据不同模式设置敌人出现机制
        switch (gameMode)
        {
            case GameMode.BeatAll:
            {
                BeatAll();
                break;
            }
            case GameMode.Survival:
            {
                Survival();
                break;
            }
            case GameMode.TreasureAll:
            {
                TreasureAll();
                break;
            }
            case GameMode.Boss:
            {
                Boss();
                break;
            }
            default:
            {
                Debug.LogWarning("You haven't set the game mode yet.");
                break;
            }
        }
    }

    private void Update()
    {

    }

    private void CreateSmallMonster()
    {
        if (NumOfSmallEnemies >= EnemyGenerator.MAX_ENEMIES)
        {
            return;
        }

        int hp = player.GetComponent<PlayerController>().currentBlood;

        if (hp <= 0)
        {
            CancelInvoke();
        }
        else
        {
            Vector3 pos = new Vector3(Random.Range(-13, 13), Random.Range(-13, 13), 0);
            RandomCreateASmallMonster(pos);
            NumOfSmallEnemies++;
            Debug.Log(NumOfSmallEnemies);
        }
    }

    private void BeatAll()
    {
        
    }

    private void Survival()
    {
        // 在限定时间内不断生成怪物
        InvokeRepeating("CreateSmallMonster", 0.5f, intervalTime);
    }

    private void TreasureAll()
    {

    }

    private void Boss()
    {

    }

    private void RandomCreateASmallMonster(Vector3 pos)
    {
        int flag = Random.Range(0, 2);

        switch (flag)
        {
            case 0:
            {
                Instantiate(flyingMonster, pos, Quaternion.identity);
                break;
            }
            case 1:
            {
                Instantiate(greenMonster, pos, Quaternion.identity);
                break;
            }
        }
    }
}
