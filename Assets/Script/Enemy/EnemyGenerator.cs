using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


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

    private float intervalTime = 8.0f; // 生成怪物的时间间隔

    static private int MAX_ENEMIES = 10; // 最大怪物数

    private GameObject flyingMonster;

    private GameObject greenMonster;

    private GameObject boss;


    public void Start()  //实例化，参数设置为坐标
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NumOfSmallEnemies = 0;
        // 获取所有敌人资源
        flyingMonster = (GameObject)Resources.Load("Prefabs/Enemy/Flying Monster");
        greenMonster = (GameObject)Resources.Load("Prefabs/Enemy/Green Monster");
        boss = (GameObject)Resources.Load("Prefabs/Enemy/Boss");


        switch (JourneyManager.getInstance().winCase)
        {
            case 0:
            {
                gameMode = GameMode.BeatAll;
                break;
            }
            case 1:
            {
                gameMode = GameMode.TreasureAll;
                break;
            }
            case 2:
            {
                gameMode = GameMode.Survival;
                break;
            }
        }

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

    public void SetGameMode(GameMode gameMode)
    {
        CancelInvoke();
        this.gameMode = gameMode;
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

    private void CreateSmallMonster()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        if (player == null || NumOfSmallEnemies >= EnemyGenerator.MAX_ENEMIES)
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
            float playerX = player.transform.position.x;
            int times = (int)((playerX + 13) / 49);
            int x = Random.Range(-13, 13);
            int y = Random.Range(-13, 13);
            x += times * 49;

            Vector3 pos = new Vector3(x, y, 0);
            RandomCreateASmallMonster(pos);
            NumOfSmallEnemies++;
            Debug.Log(NumOfSmallEnemies);
        }
    }

    private void BeatAll()
    {
        // 击败所有敌人
        intervalTime = 5.0f;
        MAX_ENEMIES = 15;
        InvokeRepeating("CreateSmallMonster", 0.5f, intervalTime);
    }

    private void Survival()
    {
        // 生存模式，在限定时间内不断生成怪物
        // 玩家存活一定时间即通关 
        intervalTime = 1.0f;
        MAX_ENEMIES = 100;
        InvokeRepeating("CreateSmallMonster", 0.5f, intervalTime);
        InvokeRepeating("SetEnemyActive", 0.5f, intervalTime / 3);
    }

    private void TreasureAll()
    {
        // 击败所有敌人
        intervalTime = 5.0f;
        MAX_ENEMIES = 15;
        InvokeRepeating("CreateSmallMonster", 0.5f, intervalTime);
    }

    private void Boss()
    {
        // Boss模式，只有一个大Boss
        Vector3 pos = new Vector3(10, 0, 0);
        Instantiate(boss, pos, Quaternion.identity);
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

    private void SetEnemyActive()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<AIDestinationSetter>().enabled = true;
        }
    }
}
