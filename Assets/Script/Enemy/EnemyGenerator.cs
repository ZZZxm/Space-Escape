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

    public int MAX_ENEMIES = 10; // 最大怪物数

    public int killedEnemy = 0; // 被杀死的敌人数量（Beat All模式使用）

    private GameObject flyingMonster;

    private GameObject greenMonster;

    private GameObject boss;

    public bool winJourney = false;


    public void Start()  //实例化，参数设置为坐标
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NumOfSmallEnemies = 0;
        killedEnemy = 0;
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
            case 3:
            {
                gameMode = GameMode.Boss;
                break;
            }
        }

        // 根据不同模式设置敌人出现机制
        SetGameMode(gameMode);
    }

    private void Update()
    {
        if (winJourney)
        {
            Invoke("WinJourney", 4.0f);
            winJourney = false;
        }
    }

    public void SetGameMode(GameMode gameMode)
    {
        CancelInvoke();
        this.gameMode = gameMode;
        killedEnemy = 0;
        Debug.Log("GameMode:" + gameMode.ToString());
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
        
        if (player == null || NumOfSmallEnemies >= MAX_ENEMIES)
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
            Vector3 pos = RandomEnemyLocation();
            RandomCreateASmallMonster(pos);
            NumOfSmallEnemies++;
            Debug.Log(NumOfSmallEnemies);
        }
    }

    private Vector3 RandomEnemyLocation()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        int roomX = (int)(Mathf.Floor((playerX + 25) / 50)) * 50;
        int roomY = (int)(Mathf.Floor((playerY + 25) / 50)) * 50;
        Debug.Log("roomX: " + roomX);
        Debug.Log("roomY: " + roomY);

        float x = Random.Range(roomX - 15.0f, roomX + 15.0f);
        float y = Random.Range(roomY - 15.0f, roomY + 15.0f);

        return new Vector3(x, y, 0);
    }

    private void BeatAll()
    {
        // 击败所有敌人
        intervalTime = 5.0f;
        MAX_ENEMIES = 3;
        InvokeRepeating("CreateSmallMonster", 0.5f, intervalTime);
    }

    private void Survival()
    {
        // 生存模式，在限定时间内不断生成怪物
        // 玩家存活一定时间即通关 
        intervalTime = 2.0f;
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
        Invoke("CreateBoss", 3.0f);
    }

    private void CreateBoss()
    {
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

    public void WinJourney()
    {
        Debug.Log("Win the BOSSS!!!");
        JourneyManager.getInstance().UnitOverToNextLevel();
    }
}
